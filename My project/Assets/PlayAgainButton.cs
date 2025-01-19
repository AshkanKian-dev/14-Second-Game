using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        // Get the Button component
        button = GetComponent<Button>();
        
        // Add click listener
        if (button != null)
        {
            button.onClick.AddListener(RestartGame);
        }
    }

    public void RestartGame()
    {
        // Reset the time scale back to normal
        Time.timeScale = 1f;
        
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
