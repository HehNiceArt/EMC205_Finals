using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

[System.Serializable]
public class EnemyType
{
    public string Name;
    public GameObject EnemyPrefab;
    [Tooltip("The amount of enemies that will spawn")]
    public int PoolSize;
}
public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager SharedInstance;
    public List<EnemyType> EnemyTypes = new List<EnemyType>();
    public List<List<GameObject>> EnemyPoolObjects = new List<List<GameObject>>();
    public Transform[] SpawnAreas;

    [Header("Enemies Prefabs")]
    public GameObject[] EnemiesPrefab;

    [Header("Spawn Parameters")]
    public float SpawnRate;
    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        InitializePools();
        StartCoroutine(SpawnEnemiesRandomly());
    }

    void InitializePools()
    {
        foreach (var enemyType  in EnemyTypes)
        {
            List<GameObject> _enemyPool = new List<GameObject>();
            for(int i = 0; i < enemyType.PoolSize; i++)
            {
                GameObject _enemy = Instantiate(enemyType.EnemyPrefab);
                _enemy.SetActive(false);
                _enemyPool.Add(_enemy);
            }
            EnemyPoolObjects.Add(_enemyPool);
        }
    }
    /// <summary>
    /// This spawns enemies from random spawn areas
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemiesRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            for(int i = 0; i < EnemyTypes.Count; i++)
            {
                GameObject _temp = GetPooledEnemies(i);
                if (_temp != null)
                {
                    int _rand = Random.Range(0, SpawnAreas.Length);
                    Transform _spawnPostion = SpawnAreas[_rand];
                    _temp.transform.position = _spawnPostion.position;
                    _temp.SetActive(true);
                }

            }
        }
    }
    public GameObject GetPooledEnemies(int enemyIndex)
    {
        for(int j = 0; j < EnemyPoolObjects[enemyIndex].Count; j++)
        {
            if (!EnemyPoolObjects[enemyIndex][j].activeInHierarchy)
            {
                return EnemyPoolObjects[enemyIndex][j];
            }
        }
        return null;
    }
}