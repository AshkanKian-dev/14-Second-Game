using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button button;
    private GameTimer gameTimer;

    void Start()
    {
        // Get the Button component
        button = GetComponent<Button>();
        gameTimer = FindObjectOfType<GameTimer>();
        
        // Add click listener
        if (button != null)
        {
            button.onClick.AddListener(RestartGame);
        }
    }

    public void RestartGame()
    {
        // Reset the timer
        if (gameTimer != null)
        {
            gameTimer.ResetTimer();
        }
        
        // Reset time scale
        Time.timeScale = 1f;
        
        // Restart background music
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.RestartBackgroundMusic();
        }
        
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
