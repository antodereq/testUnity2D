using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpPower = 14f; // Siła skoku
    [SerializeField] private Rigidbody2D _rb; // Komponent Rigidbody2D postaci
    [SerializeField] private GroundChecker groundChecker; // Komponent sprawdzający, czy postać stoi na ziemi
    // [SerializeField] private Animator animator; // Komponent Animator sterujący animacjami

    private void Update()
    {
        // animator.SetFloat("yVelocity", _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.IsGrounded())
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpPower);
            // animator.SetBool("isJumping", true);
        }

        if (groundChecker.IsGrounded() && Mathf.Abs(_rb.linearVelocity.y) < 0.1f)
        {
            // animator.SetBool("isJumping", false);
        }
    }
}
