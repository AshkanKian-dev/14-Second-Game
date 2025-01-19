using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject introPanel; // Drag your IntroPanel GameObject here
    public GameTimer gameTimer; // Reference to the GameTimer component

    void Start()
    {
        Debug.Log("IntroManager: Start called");
        // Show the intro panel at the start
        if (introPanel != null)
        {
            Debug.Log("IntroManager: Showing intro panel");
            introPanel.SetActive(true);
            Invoke("HideIntroPanel", 2f); // Hide after 2 seconds
        }
        else
        {
            Debug.LogWarning("IntroManager: introPanel is null!");
        }

        // Verify GameTimer reference
        if (gameTimer == null)
        {
            Debug.LogError("IntroManager: gameTimer reference is missing!");
        }
    }

    void HideIntroPanel()
    {
        Debug.Log("IntroManager: HideIntroPanel called");
        if (introPanel != null)
        {
            Debug.Log("IntroManager: Hiding intro panel");
            introPanel.SetActive(false);
        }
        
        // Start the timer when intro panel is hidden
        if (gameTimer != null)
        {
            Debug.Log("IntroManager: Starting the timer");
            gameTimer.StartTimer();
        }
        else
        {
            Debug.LogError("IntroManager: Cannot start timer - gameTimer is null!");
        }
    }
}
