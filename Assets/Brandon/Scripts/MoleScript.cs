using UnityEngine;

public class MoleScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float waitTime = 2f; // Time to wait at the top position

    private float startTime;
    private bool movingUp = false;
    private bool waitingAtTop = false; // Flag to track if the mole is waiting at the top
    private Vector3 startPosition;

    public bool isUp = false;

    public WhackaMole whackamole;

    void Start()
    {
        startPosition = transform.position;
    }

    public void StartMovement()
    {
        startTime = Time.time;
        movingUp = true;
        isUp = true;
    }

    void Update()
    {
        // Check if we are currently moving
        if (movingUp)
        {
            MoveUp();
        }
        else if (waitingAtTop) // Check if we are waiting at the top
        {
            WaitAtTop();
        }
    }

    // Move the object up
    void MoveUp()
    {
        float journeyLength = 3f; // Distance to move up
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        // Move up
        transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * 5f, fractionOfJourney);
        if (fractionOfJourney >= 1f)
        {
            startTime = Time.time;
            movingUp = false;
            waitingAtTop = true; // Set the waiting flag to true after moving up
        }
    }

    // Wait at the top
    void WaitAtTop()
    {
        if (Time.time - startTime >= waitTime)
        {
            // Move down
            float reverseJourneyLength = 5f; // Distance to move down
            float reverseDistCovered = (Time.time - startTime - waitTime) * moveSpeed;
            float fractionOfReverseJourney = reverseDistCovered / reverseJourneyLength;
            transform.position = Vector3.Lerp(startPosition + Vector3.up * 5f, startPosition, fractionOfReverseJourney);
            if (fractionOfReverseJourney >= 1f)
            {
                // Reset position and start moving up again
                startTime = Time.time;
                movingUp = false;
                waitingAtTop = false; // Reset waiting flag
                isUp = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            if (whackamole != null)
            {
                whackamole.playerPoints += 10;
            }

            // Start moving down immediately when hit by the hammer
            startTime = Time.time;
            movingUp = false;
            waitingAtTop = false; // Reset waiting flag
            isUp = false; // Update isUp flag

            transform.position = startPosition;
        }
    }
}
