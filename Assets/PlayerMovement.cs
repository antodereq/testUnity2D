using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 250f;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 input;

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        input = new Vector2(inputX, 0);
    }
    private void FixedUpdate()
    {
        Vector2 move = input * moveSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector2(move.x, rb.linearVelocity.y);
    }
}
