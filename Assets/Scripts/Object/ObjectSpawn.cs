using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectPrefabs;

    public int minObjectsToSpawn = 5;
    public int maxObjectsToSpawn = 10;

    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 2f;

    public float fixedSpawnHeight = 25f;

    public float minXPosition = 25f;
    public float maxXPosition = 75f;
    public float minZPosition = 25f;
    public float maxZPosition = 75f;

    void Start()
    {
        StartCoroutine(SpawnObjectsWithDelay());
    }

    IEnumerator SpawnObjectsWithDelay()
    {
        int numObjectsToSpawn = Random.Range(minObjectsToSpawn, maxObjectsToSpawn + 1);

        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();

            GameObject prefabToSpawn = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(minXPosition, maxXPosition);
        float randomZ = Random.Range(minZPosition, maxZPosition);

        return new Vector3(randomX, fixedSpawnHeight, randomZ);
    }
}