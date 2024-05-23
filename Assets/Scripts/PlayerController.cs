using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isJumping;
    private bool isFalling;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            isJumping = true;
        }

        if (rb.velocity.y < 0 && isJumping)
        {
            animator.SetTrigger("Fall");
            isJumping = false;
            isFalling = true;
        }

        if (rb.velocity.y == 0 && isFalling)
        {
            animator.SetTrigger("Idle");
            isFalling = false;
        }

        if (rb.velocity.y == 0 && !isJumping && !isFalling)
        {
            animator.SetTrigger("Idle");
        }
    }
}
