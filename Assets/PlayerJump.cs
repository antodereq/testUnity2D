using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // Siła skoku, czyli jak wysoko postać skacze
    [SerializeField] private float jumpPower = 14f;

    // Współczynnik przyspieszenia opadania - większa wartość oznacza szybsze opadanie
    [SerializeField] private float fallMultiplier = 2.5f;

    // Współczynnik przyspieszenia podczas niskiego skoku (gdy przycisk skoku zostanie puszczony wcześniej)
    [SerializeField] private float lowJumpMultiplier = 2f;

    // Referencja do komponentu Rigidbody2D postaci (fizyczne ciało)
    [SerializeField] private Rigidbody2D _rb;

    // Referencja do komponentu sprawdzającego, czy postać stoi na ziemi
    [SerializeField] private GroundChecker groundChecker;

    // Animator do kontrolowania animacji postaci
    private Animator animator;

    private void Awake()
    {
        // Pobieramy komponent Animator z GameObjectu
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Przesyłamy prędkość pionową do animatora, aby mógł sterować animacją skoku/opadania
        animator.SetFloat("yVelocity", _rb.linearVelocity.y);

        // Sprawdzamy, czy postać jest na ziemi i prawie się nie porusza w pionie
        if (groundChecker.IsGrounded() && Mathf.Abs(_rb.linearVelocity.y) < 0.1f)
        {
            // Jeśli tak, ustawiamy flagę "isJumping" na false - postać nie skacze
            animator.SetBool("isJumping", false);
        }
        else
        {
            // W przeciwnym razie ustawiamy "isJumping" na true - postać jest w powietrzu
            animator.SetBool("isJumping", true);
        }

        // Jeśli naciśnięto spację i postać stoi na ziemi, wykonujemy skok
        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.IsGrounded())
        {
            // Ustawiamy prędkość pionową na wartość siły skoku (jumpPower)
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpPower);
        }

        // Jeśli postać spada (prędkość pionowa < 0), zwiększamy siłę grawitacji, aby opadanie było szybsze
        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // Jeśli postać wznosi się, ale gracz puścił przycisk skoku, zwiększamy grawitację, by zrobić niższy skok
        else if (_rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
