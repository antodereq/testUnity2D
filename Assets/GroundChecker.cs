using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private string groundTag = "ground"; //tag ground na elementach sceny
    private bool isGrounded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
            isGrounded = false;
        }
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
}
