
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 5f;

    private float fireTimer = 0f;
    private bool facingRight = true;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        facingRight = playerMovement.transform.localScale.x > 0;

        if (Input.GetKeyDown(KeyCode.X) && fireTimer >= 1f / fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        bullet.GetComponent<PlayerBullet>().SetDirection(direction);
    }
}
