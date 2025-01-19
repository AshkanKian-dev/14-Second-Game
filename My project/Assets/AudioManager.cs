using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // For sound effects
    public AudioSource musicSource; // For background music
    public AudioSource gameOverSource; // For game over music
    public AudioSource victorySource; // For victory music
    
    public AudioClip clickSound;
    public AudioClip enemyHitSound;
    public AudioClip backgroundMusic;
    public AudioClip gameOverMusic;
    public AudioClip victoryMusic; // Add victory music clip

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        Debug.Log("AudioManager Awake");
        // Ensure we only have one AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager instance created");

            // Set up audio sources if not already present
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true; // Background music should loop
            }

            if (gameOverSource == null)
            {
                gameOverSource = gameObject.AddComponent<AudioSource>();
                gameOverSource.loop = false; // Game over music should NOT loop
            }

            if (victorySource == null)
            {
                victorySource = gameObject.AddComponent<AudioSource>();
                victorySource.loop = true; // Victory music should loop until new game
            }

            // Start playing background music
            PlayBackgroundMusic();
        }
        else
        {
            Debug.Log("Destroying duplicate AudioManager");
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        // Play sound on left click
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    public void PlayVictoryMusic()
    {
        // Stop background music
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        // Stop game over music if it's playing
        if (gameOverSource != null)
        {
            gameOverSource.Stop();
        }

        // Play victory music
        if (victoryMusic != null && victorySource != null)
        {
            victorySource.clip = victoryMusic;
            victorySource.Play();
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlayGameOverMusic()
    {
        // Stop background music
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        // Play game over music once
        if (gameOverMusic != null && gameOverSource != null)
        {
            gameOverSource.clip = gameOverMusic;
            gameOverSource.loop = false; // Make sure it doesn't loop
            gameOverSource.Play();
        }
    }

    public void RestartBackgroundMusic()
    {
        // Stop game over music
        if (gameOverSource != null)
        {
            gameOverSource.Stop();
        }

        // Stop victory music
        if (victorySource != null)
        {
            victorySource.Stop();
        }

        // Restart background music
        PlayBackgroundMusic();
    }

    public void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void PlayEnemyHitSound()
    {
        Debug.Log("PlayEnemyHitSound called");
        if (enemyHitSound != null && audioSource != null)
        {
            Debug.Log("Playing enemy hit sound");
            audioSource.PlayOneShot(enemyHitSound);
        }
    }
}
