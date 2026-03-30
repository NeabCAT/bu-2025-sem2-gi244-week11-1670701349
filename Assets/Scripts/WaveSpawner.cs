using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataWave
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}

public class WaveSpawner : MonoBehaviour
{
    public DataWave[] waves;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public Transform[] spawnPoints; 

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        foreach (DataWave wave in waves)
        {
            yield return StartCoroutine(SpawnWave(wave));

            yield return new WaitUntil(() => FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length == 0);
        }
    }

    IEnumerator SpawnWave(DataWave wave)
    {

        List<Transform> selectedPoints = GetRandomSpawnPoints(wave.numberOfRandomSpawnPoint);

        for (int i = 0; i < wave.numberOfPowerUp; i++)
        {
            Transform point = selectedPoints[Random.Range(0, selectedPoints.Count)];
            Instantiate(powerUpPrefab, point.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(wave.delayStart);

        for (int i = 0; i < wave.totalSpawnEnemies; i++)
        {
            Transform point = selectedPoints[Random.Range(0, selectedPoints.Count)];
            Instantiate(enemyPrefab, point.position, Quaternion.identity);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    List<Transform> GetRandomSpawnPoints(int count)
    {
        List<Transform> allPoints = new List<Transform>(spawnPoints);
        List<Transform> selected = new List<Transform>();

        for (int i = 0; i < count; i++)
        {
            int randIndex = Random.Range(0, allPoints.Count);
            selected.Add(allPoints[randIndex]);
            allPoints.RemoveAt(randIndex); // äÁè«éÓ
        }

        return selected;
    }
}