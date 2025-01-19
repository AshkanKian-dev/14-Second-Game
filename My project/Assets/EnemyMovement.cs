using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 randomDirection;

    void Start()
    {
        moveSpeed = Random.Range(1f, 3f);
        float randomAngle = Random.Range(0f, 360f);
        randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    void Update()
    {
        transform.position += (Vector3)randomDirection * moveSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            // Play hit sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayEnemyHitSound();
            }

            // Play hit particle effect
            if (ParticleManager.Instance != null)
            {
                ParticleManager.Instance.PlayHitEffect(transform.position);
            }

            // Only destroy the enemy
            Destroy(gameObject);
        }
        else if (other.CompareTag("Boundary"))
        {
            // Reverse direction when hitting a boundary
            moveSpeed = -moveSpeed;
            // Flip the sprite
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    public void StartChasingArrow(GameObject arrow)
    {
        randomDirection = (arrow.transform.position - transform.position).normalized;
    }
}
