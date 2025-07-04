using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target; // Tutaj przypinasz gracza
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Dystans nad g³ow¹

    void Update()
    {
        // Ustawiamy pozycjê slidera nad graczem bez zmiany rotacji
        transform.position = target.position + offset;
    }
}
