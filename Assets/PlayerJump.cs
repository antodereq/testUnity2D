using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundChecker groundChecker; //aby podczepiæ skrypt GroundChecker w inspectorze

    private bool isJumping = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.IsGrounded()) //gdy naciœniêto spacjê i gracz jest na ziemi
        {
            isJumping = true;
        }
    }
    private void FixedUpdate()
    {
        if(isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
    }
}
