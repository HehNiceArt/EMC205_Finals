using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveSystem : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public float TimeBetweenWaves = 10f;
    public int InitialEnemiesPerWave = 5;
    public int EnemiesPerWaveIncrease = 2;
    public GameObject EnemyHolder;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        int _currentWave = 1;

        while (true)
        {
            yield return new WaitForSeconds(TimeBetweenWaves);

            for(int i = 0; i < InitialEnemiesPerWave + (_currentWave - 1) * EnemiesPerWaveIncrease; i++)
            {
                GameObject _enemyPrefab = GetRandomEnemyPrefab();
                GameObject _spawnPoint = GetRandomSpawnPoint();

                //GameObject _enemy = EnemyPooling_2.Instance.GetPooledObject(_enemyPrefab.name);
                //_enemy.transform.position = _spawnPoint.transform.position;
                //_enemy.transform.parent = EnemyHolder.transform;
            }
            _currentWave++;
        }
    }

    GameObject GetRandomEnemyPrefab()
    {
        int _rand = Random.Range(0, EnemyPooling_2.Instance.EnemyPrefab.Count);
        return EnemyPooling_2.Instance.EnemyPrefab[_rand];
    }
    GameObject GetRandomSpawnPoint()
    {
        if(SpawnPoints.Length > 0)
        {
            int _rand = Random.Range(0, SpawnPoints.Length);
            return SpawnPoints[_rand];
        }
        else
        {
            Debug.LogError("No Spawn Points Available.");
            return null;
        }
    }
}