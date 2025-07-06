using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 2f; // 2 strza³y na sekundê

    private float fireCooldown = 0f;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bool facingRight = playerMovement.IsFacingRight;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        bullet.GetComponent<PlayerBullet>().SetDirection(direction);
    }
}
