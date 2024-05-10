using UnityEngine;

public class Bobber : MonoBehaviour
{
    public Transform rod; // Reference to the fishing rod
    public float followDelay = 0.5f; // Delay for the bobber to follow the rod
    public float maxTensionDistance = 2f; // Maximum distance before string tension applies
    public float tensionStrength = 10f; // Strength of the tension force

    private Rigidbody rb;
    private Vector3 targetPosition;

    public Fishing fishing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Calculate the target position for the bobber with delay
        targetPosition = Vector3.Lerp(targetPosition, rod.position - rod.up * 2f, followDelay * Time.deltaTime);

        // Calculate the direction vector between the rod and the bobber
        Vector3 direction = targetPosition - transform.position;

        // Move the bobber directly towards the calculated target position
        rb.MovePosition(transform.position + direction.normalized * Mathf.Min(direction.magnitude, maxTensionDistance));

        // Apply tension force if the bobber is too far from the rod
        if (direction.magnitude > maxTensionDistance)
        {
            float excessDistance = direction.magnitude - maxTensionDistance;
            Vector3 tensionForce = direction.normalized * excessDistance * tensionStrength;
            rb.AddForce(tensionForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Hit fish");
            Fish fish = other.GetComponent<Fish>();

            if (fish.goldenFish)
            {
                fishing.score += 20;
            }

            else
            {
                fishing.score += 10;
            }

            other.gameObject.SetActive(false);
        }
    }
}
