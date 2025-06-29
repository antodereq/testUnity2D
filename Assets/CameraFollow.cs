using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;

    // Domy�lny offset kamery - utrzymuje kamer� na odpowiedniej wysoko�ci i odsuni�ciu
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -10f);

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ustawiamy pozycj� kamery na wyg�adzon� pozycj�
        transform.position = smoothedPosition;
    }
}
