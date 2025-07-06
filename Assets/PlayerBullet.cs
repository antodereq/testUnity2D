using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        rb.linearVelocity = direction * speed;

        Vector3 scale = transform.localScale;
        scale.x = direction.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().TakeDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        }
    }
}
