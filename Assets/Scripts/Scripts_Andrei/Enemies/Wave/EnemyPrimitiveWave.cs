using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrimitiveWave : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private GameObject[] _spawnPoints;

    [Header("Variables")]
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private int _enemiesPerWave = 5;
    [SerializeField] private float _timeBetweenEnemies = 1f;
    [SerializeField] private float _increaseDifficultyRate = 0.1f;

    int _currentWave = 1;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenWaves); 
            for(int i = 0; i < _currentWave * _enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(_timeBetweenEnemies);
            }
            _currentWave++;
            _timeBetweenEnemies -= _increaseDifficultyRate;
        }
    }
    void SpawnEnemy()
    {
        int _randomEnemyIndex = Random.Range(0, _enemyPrefab.Length);
        int _randomSpawnPointIndex = Random.Range(0, _spawnPoints.Length);

        Instantiate(_enemyPrefab[_randomEnemyIndex], _spawnPoints[_randomSpawnPointIndex].transform);
    }

}
