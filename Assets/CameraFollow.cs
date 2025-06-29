using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;

    // Domyœlny offset kamery - utrzymuje kamerê na odpowiedniej wysokoœci i odsuniêciu
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -10f);

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ustawiamy pozycjê kamery na wyg³adzon¹ pozycjê
        transform.position = smoothedPosition;
    }
}
