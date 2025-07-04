using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target; // Tutaj przypinasz gracza
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Dystans nad g�ow�

    void Update()
    {
        // Ustawiamy pozycj� slidera nad graczem bez zmiany rotacji
        transform.position = target.position + offset;
    }
}
