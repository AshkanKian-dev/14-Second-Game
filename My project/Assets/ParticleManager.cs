using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject hitParticlePrefab;
    private static ParticleManager instance;
    public static ParticleManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayHitEffect(Vector3 position)
    {
        if (hitParticlePrefab != null)
        {
            // Instantiate the particle effect
            GameObject effect = Instantiate(hitParticlePrefab, position, Quaternion.identity);
            
            // Destroy the particle system after its duration
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(effect, ps.main.duration);
            }
            else
            {
                Destroy(effect, 1f); // Fallback destruction after 1 second
            }
        }
    }
}
