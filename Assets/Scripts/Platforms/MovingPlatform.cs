using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Transform pointA;  // Starting point
    public Transform pointB;  // Ending point
    public float speed = 2.0f;  // Movement speed
    private Vector3 destination;

    private void Start()
    {
        // Set initial destination to point B
        destination = pointB.position;
    }

    private void Update()
    {
        // Move the platform towards the destination point
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        // Check if the platform has reached the destination
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            // Swap the destination between point A and point B
            destination = destination == pointA.position ? pointB.position : pointA.position;
        }
    }
}