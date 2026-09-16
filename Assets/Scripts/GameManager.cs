using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float scoreMultiplier = 10f;
    public GameObject explosionPrefab;

    // Bonus: Increase difficulty over time
    public float difficultyIncrease = 0.05f;

    private float survivalTime;
    private Label scoreLabel;
    private Button restartButton;
    private bool gameOver = false;

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        scoreLabel =
            uiDocument.rootVisualElement.Q<Label>("ScoreLabel");

        restartButton =
            uiDocument.rootVisualElement.Q<Button>("RestartButton");

        if (restartButton != null)
        {
            restartButton.style.display = DisplayStyle.None;
            restartButton.clicked += RestartGame;
        }
    }

    void Update()
    {
        if (gameOver)
            return;

        survivalTime += Time.deltaTime;

        int score =
            Mathf.FloorToInt(survivalTime * scoreMultiplier);

        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + score;
        }

        IncreaseDifficulty();
    }

    void IncreaseDifficulty()
    {
        Obstacle[] obstacles =
            FindObjectsByType<Obstacle>(FindObjectsSortMode.None);

        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.IncreaseSpeed(
                survivalTime,
                difficultyIncrease
            );
        }
    }

    public void GameOver(Vector3 playerPosition)
    {
        if (gameOver)
            return;

        gameOver = true;

        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                playerPosition,
                Quaternion.identity
            );
        }

        if (restartButton != null)
        {
            restartButton.style.display =
                DisplayStyle.Flex;
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}