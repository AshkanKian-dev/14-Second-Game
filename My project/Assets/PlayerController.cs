using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject arrow;
    public float shootSpeed = 10f;
    private Vector3 arrowStartPosition;
    private bool arrowIsFired = false;
    private bool canMove = true;
    public int score = 0;
    public const int SCORE_TO_WIN = 5;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endMessageText;
    public TextMeshProUGUI gameOverText;
    public OutroManager outroManager; // Reference to OutroManager
    public GameTimer gameTimer;

    void Start()
    {
        // Initialize variables and hide UI elements
        arrowStartPosition = arrow.transform.localPosition;
        if (scoreText != null) scoreText.text = "Score: 0";
        if (endMessageText != null) endMessageText.gameObject.SetActive(false);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (canMove)
        {
            HandleMovement();
        }
        HandleArrow();

        if (arrowIsFired)
        {
            CheckArrowReturn();
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveX, moveY, 0).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    void HandleArrow()
    {
        if (Input.GetMouseButtonDown(0) && !arrowIsFired && canMove)
        {
            ShootArrow();
        }
    }

    void ShootArrow()
    {
        var rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.up * shootSpeed;
            arrowIsFired = true;
            canMove = false;
        }
    }

    void CheckArrowReturn()
    {
        if (arrow == null) return;

        Vector3 screenPos = Camera.main.WorldToViewportPoint(arrow.transform.position);
        if (screenPos.y > 1 || screenPos.y < 0 || screenPos.x > 1 || screenPos.x < 0)
        {
            ResetArrow();
        }
    }

    void ResetArrow()
    {
        var rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        arrow.transform.localPosition = arrowStartPosition;
        arrowIsFired = false;
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            score++;
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
            Destroy(collision.gameObject);

            if (score >= SCORE_TO_WIN)
            {
                Win();
            }
        }
    }

    void Win()
    {
        if (endMessageText != null)
        {
            endMessageText.gameObject.SetActive(true);
            endMessageText.text = "YOU WIN!";
        }

        // Freeze gameplay immediately
        Time.timeScale = 0f;

        // Play victory music
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayVictoryMusic();
        }

        // Start the delayed outro coroutine
        if (outroManager != null)
        {
            StartCoroutine(ShowOutroDelayed());
        }
    }

    IEnumerator ShowOutroDelayed()
    {
        // Wait for 1 second (real time, not affected by timeScale)
        yield return new WaitForSecondsRealtime(1f);
        
        if (outroManager != null)
        {
            outroManager.PlayOutro();
        }
    }
}
