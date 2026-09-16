using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float thrust = 5f;

    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get mouse position
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world position
        mousePosition.z = -mainCamera.transform.position.z;

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        // Calculate direction to mouse
        Vector2 direction =
            worldPosition - transform.position;

        // Rotate player toward mouse
        if (direction != Vector2.zero)
        {
            float angle =
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation =
                Quaternion.Euler(0f, 0f, angle - 90f);
        }

        // Apply thrust while holding left mouse button
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(transform.up * thrust);
        }
    }

    void FixedUpdate()
    {
        // Keep the player inside the game borders
        Vector2 position = rb.position;

        position.x = Mathf.Clamp(position.x, -8.6f, 8.6f);
        position.y = Mathf.Clamp(position.y, -4.6f, 4.6f);

        rb.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only destroy the player when hitting an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager gameManager =
                FindFirstObjectByType<GameManager>();

            if (gameManager != null)
            {
                gameManager.GameOver(transform.position);
            }

            Destroy(gameObject);
        }
    }
}