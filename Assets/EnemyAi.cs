using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase }

    [Header("Ruch patrolowy")]
    public float patrolRange = 3f;
    public float moveSpeed = 2f;

    [Header("Wykrywanie gracza")]
    public float visionDistance = 7f;

    [Header("Agro")]
    public float chaseStopDistance = 10f;

    [Header("Strzelanie")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 1f;

    private Transform player;
    private Vector3 patrolStartPoint;
    private Vector3 leftPoint, rightPoint;
    private EnemyState currentState = EnemyState.Idle;

    private SpriteRenderer spriteRenderer;
    private float fireTimer = 0f;
    private bool movingRight = true;
    private bool facingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        patrolStartPoint = transform.position;
        leftPoint = patrolStartPoint - Vector3.right * patrolRange;
        rightPoint = patrolStartPoint + Vector3.right * patrolRange;

        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeColor(Color.green);

        Invoke(nameof(StartPatrolling), 2f);
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                DetectPlayer();
                break;

            case EnemyState.Chase:
                ChasePlayer();
                break;

            case EnemyState.Idle:
                break;
        }
    }

    void StartPatrolling()
    {
        currentState = EnemyState.Patrol;
        ChangeColor(Color.yellow);
    }

    void Patrol()
    {
        Vector3 target = movingRight ? rightPoint : leftPoint;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            movingRight = !movingRight;

            // Ustaw kierunek patrzenia zgodnie z ruchem
            if (movingRight && !facingRight)
                Flip();
            else if (!movingRight && facingRight)
                Flip();
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= visionDistance)
        {
            currentState = EnemyState.Chase;
            ChangeColor(Color.red);
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (fireTimer >= 1f / fireRate && distanceToPlayer <= visionDistance)
        {
            ShootAtPlayer();
            fireTimer = 0f;
        }

        if (distanceToPlayer > chaseStopDistance)
        {
            currentState = EnemyState.Patrol;
            ChangeColor(Color.yellow);
        }
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Vector2 direction = (player.position - shootPoint.position).normalized;

        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
