using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 10f;
    public TextMeshProUGUI timerText;
    private bool timerRunning = false;
    private bool gameOverTriggered = false;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned!");
            return;
        }

        ResetTimer();
        Debug.Log("GameTimer: Initialized with duration: " + gameDuration);
    }

    void Update()
    {
        if (!timerRunning || gameOverTriggered) return;

        gameDuration -= Time.deltaTime;
        
        if (gameDuration <= 0)
        {
            Debug.Log("GameTimer: Time's up! Setting to game over state");
            EndGame();
        }
        else
        {
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText == null) return;

        // If game is over, always show 00:00
        if (gameOverTriggered)
        {
            timerText.text = "00:00";
            timerText.color = Color.red;
            return;
        }

        int seconds = Mathf.Max(0, Mathf.FloorToInt(gameDuration));
        timerText.text = $"00:{seconds:00}";
        Debug.Log($"GameTimer: Updating display to {timerText.text}");

        if (gameDuration <= 3f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    void EndGame()
    {
        if (gameOverTriggered) return;
        Debug.Log("GameTimer: EndGame called");
        
        // Set timer to exactly zero
        gameDuration = 0;
        timerRunning = false;
        gameOverTriggered = true;

        // Force display to show 00:00
        if (timerText != null)
        {
            timerText.text = "00:00";
            timerText.color = Color.red;
            Debug.Log("GameTimer: Forced display to 00:00");
        }

        // Check if player has won
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null && player.score < PlayerController.SCORE_TO_WIN)
        {
            if (GameOverManager.Instance != null)
            {
                Debug.Log("GameTimer: Triggering game over screen");
                GameOverManager.Instance.ShowGameOver();
            }
        }
    }

    void ResetTimer()
    {
        // Don't reset if game is over
        if (gameOverTriggered)
        {
            Debug.Log("GameTimer: Attempted to reset while game over - ignored");
            return;
        }

        Debug.Log("GameTimer: Resetting timer");
        gameDuration = 10f;
        timerRunning = false;
        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        if (gameOverTriggered)
        {
            Debug.Log("GameTimer: Can't start timer - game is over");
            return;
        }
        
        Debug.Log("GameTimer: Starting timer");
        gameDuration = 10f;
        timerRunning = true;
        UpdateTimerDisplay();
    }

    public void ResetGame()
    {
        Debug.Log("GameTimer: Resetting game");
        gameOverTriggered = false;
        gameDuration = 10f;
        timerRunning = false;
        UpdateTimerDisplay();
    }
}
