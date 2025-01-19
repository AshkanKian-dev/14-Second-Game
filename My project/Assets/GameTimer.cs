using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 10f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    private bool timerRunning = false;
    private bool gameOverTriggered = false;
    private float initialDuration;

    void Start()
    {
        Debug.Log("GameTimer: Start called");
        initialDuration = gameDuration;
        UpdateTimerUI();
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (timerRunning && !gameOverTriggered && gameDuration > 0)
        {
            gameDuration -= Time.deltaTime;
            if (gameDuration <= 0)
            {
                gameDuration = 0;
                EndGame();
            }
            UpdateTimerUI();
        }
    }

    public void StartTimer()
    {
        Debug.Log("GameTimer: StartTimer called");
        if (!gameOverTriggered)
        {
            gameDuration = initialDuration;
            timerRunning = true;
            UpdateTimerUI();
            Debug.Log($"GameTimer: timerRunning set to {timerRunning}");
        }
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(gameDuration);
        timerText.text = $"00:{seconds:00}";
        
        // Change color to red when timer is below 3 seconds or at game over
        if (gameDuration <= 3f || gameOverTriggered)
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
        if (!gameOverTriggered)
        {
            Debug.Log("EndGame called");
            gameOverTriggered = true;
            timerRunning = false;
            gameDuration = 0f; // Force timer to zero
            UpdateTimerUI(); // Update UI to show zero
            
            // Only show game over if player hasn't won
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null && player.score < PlayerController.SCORE_TO_WIN)
            {
                Time.timeScale = 0f;
                if (gameOverText != null)
                {
                    gameOverText.gameObject.SetActive(true);
                    gameOverText.text = "Game Over!";
                    Debug.Log("Game Over text shown");
                }
                else
                {
                    Debug.LogWarning("gameOverText is null!");
                }

                // Play game over music
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayGameOverMusic();
                }
            }
        }
    }

    public void ResetTimer()
    {
        gameDuration = initialDuration;
        timerRunning = false;
        gameOverTriggered = false;
        UpdateTimerUI();
        Time.timeScale = 1f;
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }
}
