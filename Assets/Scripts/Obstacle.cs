using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float baseSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Random obstacle size
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * randomSize;

        // Random direction
        float randomAngle = Random.Range(0f, 360f);

        direction = new Vector2(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad),
            Mathf.Sin(randomAngle * Mathf.Deg2Rad)
        ).normalized;

        // Random starting speed
        baseSpeed = Random.Range(minSpeed, maxSpeed);

        rb.AddForce(
            direction * baseSpeed,
            ForceMode2D.Impulse
        );
    }

    // Bonus: Increase obstacle speed over time
    public void IncreaseSpeed(float time, float difficultyIncrease)
    {
        float newSpeed =
            baseSpeed + (time * difficultyIncrease);

        newSpeed = Mathf.Min(newSpeed, 7f);

        Vector2 currentDirection = rb.linearVelocity.normalized;

        if (currentDirection != Vector2.zero)
        {
            rb.linearVelocity =
                currentDirection * newSpeed;
        }
    }
}