using UnityEngine;

public class Zadanie3 : MonoBehaviour
{
    public float speed = 2.0f;   
    private float length = 10.0f;
    private int side = 0; 
    private Vector3 startPosition;  
    private float distance = 0.0f; 

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * movement);
        distance += movement;

        if (distance >= length)
        {
            transform.Rotate(0, 90, 0);

            distance = 0.0f;

            side = (side + 1) % 4;
        }
    }
}