using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Ruch")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolMinX;  // minimalna pozycja X, np. lewy koniec platformy
    [SerializeField] private float patrolMaxX;  // maksymalna pozycja X, np. prawy koniec platformy
    [SerializeField] private float pointReachThreshold = 0.2f; // odległość do celu, żeby wybrać nowy punkt
    [SerializeField] private float directionDeadzone = 0.05f; // deadzone dla kierunku ruchu

    [Header("Agro")]
    [SerializeField] private Transform player;
    [SerializeField] private float visionRange = 5f;
    [SerializeField] private float loseAgroDistance = 7f;

    private Rigidbody2D rb;
    private Vector2 currentTargetPoint;
    private bool isAgro = false;

    // Do obracania sprite'a
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ustaw przeciwnika na losową pozycję startową na osi X między min a max
        float startX = Random.Range(patrolMinX, patrolMaxX);
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);

        ChooseNewPatrolPoint();
    }


    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (isAgro)
        {
            if (distToPlayer > loseAgroDistance)
                isAgro = false; // stracił agro
        }
        else
        {
            if (distToPlayer <= visionRange)
                isAgro = true; // widzi gracza
        }
    }

    void FixedUpdate()
    {
        float dir = 0f;

        if (isAgro)
        {
            dir = GetDirection(player.position.x, transform.position.x, directionDeadzone);
        }
        else
        {
            dir = GetDirection(currentTargetPoint.x, transform.position.x, directionDeadzone);

            if (Mathf.Abs(transform.position.x - currentTargetPoint.x) < pointReachThreshold)
            {
                ChooseNewPatrolPoint();
            }
        }


        // Ustaw prędkość poziomą lub zatrzymaj jeśli dir == 0
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        // Obracanie przeciwnika w kierunku ruchu
        if (dir > 0)
            spriteRenderer.flipX = false; // idzie w prawo - domyślny kierunek
        else if (dir < 0)
            spriteRenderer.flipX = true;  // idzie w lewo - odwrócony sprite
    }

    float GetDirection(float targetX, float currentX, float deadzone)
    {
        float diff = targetX - currentX;
        if (Mathf.Abs(diff) < deadzone)
            return 0f;
        return Mathf.Sign(diff);
    }

    void ChooseNewPatrolPoint()
    {
        float x = Random.Range(patrolMinX, patrolMaxX);
        currentTargetPoint = new Vector2(x, transform.position.y);
    }
}
