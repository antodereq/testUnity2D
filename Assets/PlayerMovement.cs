using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 250f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private Vector2 input;
    private bool facingRight = true;

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        input = new Vector2(inputX, 0);

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));

        // Flipowanie postaci
        if (input.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (input.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = input * moveSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector2(move.x, rb.linearVelocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
