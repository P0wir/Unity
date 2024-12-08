using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcSpawner : MonoBehaviour
{
    public GameObject orcPrefab; 
    public Transform[] spawnPoints; 
    public bool useSpawnArea = true; 
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); 

    public int orcsPerWave = 5; 
    public float spawnInterval = 3f; 

    private float spawnTimer = 0f; 

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnOrcs();
            spawnTimer = 0f; 
        }
    }

    void SpawnOrcs()
    {
        for (int i = 0; i < orcsPerWave; i++)
        {
            Vector3 spawnPosition;

            if (useSpawnArea)
            {
                spawnPosition = GetRandomSpawnPosition();
            }
            else if (spawnPoints != null && spawnPoints.Length > 0)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                spawnPosition = spawnPoint.position;
            }
            else
            {
                Debug.LogError("No spawn area or spawn points assigned!");
                return;
            }

            Instantiate(orcPrefab, spawnPosition, Quaternion.identity);
        }

        Debug.Log($"Spawned {orcsPerWave} orcs at random locations.");
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);

        return new Vector3(randomX, randomY, 0) + transform.position;
    }

}
