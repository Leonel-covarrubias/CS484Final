using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 2f;
    private float radius = 2f;

    private Vector3 centerPosition;
    private float angle = 0f;

    private float changeTimer = 4f;
    private float interval = 0f;
    private float currentRadius;

    public bool goldenFish;

    void Start()
    {
        centerPosition = transform.position;
        currentRadius = radius; // Initialize current radius
    }

    void Update()
    {
        interval += Time.deltaTime;
        if (interval >= changeTimer)
        {
            // Set a random radius between 2 and 5
            currentRadius = Random.Range(2f, 5f);
            interval = 0f;
        }

        // Increment the angle over time
        angle += speed * Time.deltaTime;

        // Calculate the new position of the object along the circle
        float x = centerPosition.x + Mathf.Cos(angle) * currentRadius;
        float z = centerPosition.z + Mathf.Sin(angle) * currentRadius;

        // Update the object's position gradually
        Vector3 newPosition = new Vector3(x, transform.position.y, z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);

        // Calculate the direction the fish is moving and add an extra 90 degrees
        Vector3 movementDirection = newPosition - transform.position;
        movementDirection.y = 0f; // Ensure fish stays in the horizontal plane
        Quaternion additionalRotation = Quaternion.AngleAxis(90f, Vector3.up);
        movementDirection = additionalRotation * movementDirection;

        // Rotate the fish to face forward along its path of movement
        if (movementDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
        }
    }


}
