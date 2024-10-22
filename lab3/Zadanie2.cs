using UnityEngine;

public class Zadanie2 : MonoBehaviour
{
    public float speed = 2.0f;   
    private Vector3 startPosition; 
    private bool right = true;

    private float distanceMoved = 0.0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float velocity = speed * Time.deltaTime;

        if (right)
        {
            transform.Translate(Vector3.right * velocity);
            distanceMoved += velocity;

            if (distanceMoved >= 10.0f)
            {
                right = false;
                distanceMoved = 0.0f; 
            }
        }
        else
        {
            transform.Translate(Vector3.left * velocity);
            distanceMoved += velocity;

            if (distanceMoved >= 10.0f)
            {
                right = true;
                distanceMoved = 0.0f; 
            }
        }
    }
}