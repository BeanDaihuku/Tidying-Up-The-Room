using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("ê›íË")]
    [SerializeField] private GameObject bearPrefab; 
    [SerializeField] private Transform[] spawnPoints; 
    [SerializeField] private float spawnInterval = 3.0f; 

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true) 
        {
            SpawnBear();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBear()
    {
        if (spawnPoints.Length == 0) return;

        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(bearPrefab, spawnPoints[index].position, Quaternion.identity);
    }
}