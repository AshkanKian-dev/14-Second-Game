using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    public GameObject outroPanel; // Drag your OutroPanel GameObject here
    public Button playAgainButton;
    private Image panelImage;

    void Start()
    {
        // Make sure the outro panel is hidden at start
        if (outroPanel != null)
        {
            outroPanel.SetActive(false);
            panelImage = outroPanel.GetComponent<Image>();
        }

        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(PlayAgain);
        }
    }

    public void PlayOutro()
    {
        if (outroPanel != null)
        {
            outroPanel.SetActive(true);
            // Fade in the panel over 2 seconds
            StartCoroutine(FadeInPanel());
        }
    }

    public void PlayAgain()
    {
        // Reset the time scale back to normal
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

    IEnumerator FadeInPanel()
    {
        float elapsedTime = 0f;
        float duration = 2f;
        Color startColor = panelImage.color;
        startColor.a = 0f;
        Color endColor = startColor;
        endColor.a = 1f;
        panelImage.color = startColor;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            Color newColor = panelImage.color;
            newColor.a = alpha;
            panelImage.color = newColor;
            yield return null;
        }

        // Ensure we end up with full opacity
        panelImage.color = endColor;
    }
}
