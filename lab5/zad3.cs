using UnityEngine;

public class MultiPointPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    private int currentPoint = 0;
    private bool reverse = false;

    private void Update()
    {
        if (waypoints.Length == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[currentPoint].position) < 0.1f)
        {
            if (!reverse)
            {
                currentPoint++;
                if (currentPoint >= waypoints.Length)
                {
                    currentPoint = waypoints.Length - 2;
                    reverse = true;
                }
            }
            else
            {
                currentPoint--;
                if (currentPoint < 0)
                {
                    currentPoint = 1;
                    reverse = false;
                }
            }
        }
    }
}
