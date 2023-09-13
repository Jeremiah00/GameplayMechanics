using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefab;
    float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 3;
    public GameObject[] powerupPrefabs;
    void Start()
    {
        int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefabs[randomPowerUp], GenerateSpawnPosition(), powerupPrefabs[randomPowerUp].transform.rotation);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawbPosX = Random.Range(-spawnRange, spawnRange);
        float spawbPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randonPos = new Vector3(spawbPosX, 0, spawbPosZ);
        return randonPos;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerUp], GenerateSpawnPosition(), powerupPrefabs[randomPowerUp].transform.rotation);
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }
}
