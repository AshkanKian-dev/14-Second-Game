using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }
    
    [Header("UI References")]
    public TextMeshProUGUI gameOverText;
    public Button resetButton;
    public Canvas gameOverCanvas;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ensure UI is hidden at start
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (resetButton != null)
        {
            resetButton.gameObject.SetActive(false);
            resetButton.onClick.AddListener(ResetGame);
        }
        
        // Set up canvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.sortingOrder = 999; // Ensure it's on top
            gameOverCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Stop the game
        Time.timeScale = 0f;

        // Show UI
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER!";
            gameOverText.color = Color.red;
        }

        if (resetButton != null)
        {
            resetButton.gameObject.SetActive(true);
        }

        // Play sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameOverMusic();
        }
    }

    public void ResetGame()
    {
        // Reset time scale
        Time.timeScale = 1f;
        isGameOver = false;

        // Hide UI
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (resetButton != null) resetButton.gameObject.SetActive(false);

        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
