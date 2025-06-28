using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
