using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance for global access
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private Text scoreText;  // Reference to the UI Text element for displaying score
    private int score = 0;                    // Initial score

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            // Optionally, call DontDestroyOnLoad(gameObject) if you want the score to persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    // Method to add points to the score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // Updates the UI Text to reflect the current score
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}