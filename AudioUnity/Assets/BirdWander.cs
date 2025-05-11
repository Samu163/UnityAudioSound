using UnityEngine;

public class BirdWanderFixedHeight : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 2f;
    public float changeDirectionInterval = 3f;
    public float flightBounds = 20f;
    public float flightHeight = 10f;

    private Vector3 targetDirection;
    private float timeToChangeDirection;

    void Start()
    {
        PickNewDirection();
        Vector3 startPos = transform.position;
        transform.position = new Vector3(startPos.x, flightHeight, startPos.z);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, flightHeight, transform.position.z);

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        timeToChangeDirection -= Time.deltaTime;
        if (timeToChangeDirection <= 0)
        {
            PickNewDirection();
        }
        Vector3 flatPos = new Vector3(transform.position.x, 0, transform.position.z);
        if (flatPos.magnitude > flightBounds)
        {
            targetDirection = -flatPos.normalized;
        }
    }

    void PickNewDirection()
    {
        targetDirection = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;

        timeToChangeDirection = changeDirectionInterval;
    }
}
