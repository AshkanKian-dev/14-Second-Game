using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float minX = -5f, maxX = 5f;
    public List<GameObject> enemyList = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyList.Add(enemy);
    }

    public void MakeEnemiesChaseArrow(GameObject arrow)
    {
        foreach (var enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyMovement>().StartChasingArrow(arrow);
            }
        }
    }
}
