using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[System.Serializable]
public class EnemyType
{
    public string Name;
    public GameObject EnemyPrefab;
    [Tooltip("The amount of enemies that will spawn")]
    public int PoolSize;
}
[System.Serializable]
public class EnemyPoolList
{
    public List<GameObject> EnemyPool = new List<GameObject>();
}
public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager SharedInstance;
    public List<EnemyType> EnemyTypes = new List<EnemyType>();
    public Transform[] SpawnAreas;

    [Header("Enemies Spawn")]
    public GameObject EnemySpawn;

    [Header("Spawn Parameters")]
    public float SpawnRate;

    [Header("Enemy Pool")]
    [SerializeField]
    private List<EnemyPoolList> EnemyPoolObjects = new List<EnemyPoolList>();

    [Header("Wave")]
    [SerializeField] private int _currentWave = 0;
    public bool IsSpawning = false;

    public static EnemyPoolManager Instance;
    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        InitializePools();
        StartWave();
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
            EnemyPoolList _enemyPoolList = new EnemyPoolList { EnemyPool = _enemyPool };
            EnemyPoolObjects.Add(_enemyPoolList);
        }
    }
    void StartWave()
    {
        IsSpawning = true;
        StartCoroutine(SpawnEnemies());
    }

    public void StartNextWaveButtonPress()
    {
        foreach(var poolList in EnemyPoolObjects)
        {
            foreach(var enemy in poolList.EnemyPool)
            {
                if (!IsSpawning && !enemy.activeInHierarchy)
                {
                    Debug.Log($"Enemies Active? {enemy.activeInHierarchy}");
                    StartNextWave();
                }
            }
        }
    }
    public void StartNextWave()
    {
        StopCoroutine(SpawnEnemies());
        _currentWave++;

        IsSpawning = true;
        StartCoroutine(SpawnEnemies());
    }
    /// <summary>
    /// This spawns enemies from random spawn areas
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemies()
    {
        while (IsSpawning)
        {
            yield return new WaitForSeconds(SpawnRate);
            if(AllEnemiesDefeated())
            {
                Debug.Log($"Current Wave: {_currentWave}");
                SpawnEnemiesRandomly();
            }
        }
    }
    void SpawnEnemiesRandomly()
    {
        for (int i = 0; i < EnemyTypes.Count; i++)
        {
            GameObject _temp = GetPooledEnemies(i);
            if (_temp != null)
            {
                int _rand = Random.Range(0, SpawnAreas.Length);
                Transform _spawnPostion = SpawnAreas[_rand];
                _temp.transform.position = _spawnPostion.position;
                _temp.SetActive(true);
                _temp.transform.parent = EnemySpawn.transform;
            }
        }
    }
    public bool AllEnemiesDefeated()
    {
        bool _allDefeated = true;

        foreach(var poolList in EnemyPoolObjects)
        {
            foreach (var enemy in poolList.EnemyPool)
            {
                if(enemy.activeInHierarchy) 
                {
                    _allDefeated = false;
                    break;
                }
                if(!_allDefeated)
                {
                    break;
                }
            }
        }
        if(_allDefeated)
        {
            IsSpawning = false;
        }

        return _allDefeated;
    }
    public GameObject GetPooledEnemies(int enemyIndex)
    {
        for(int j = 0; j < EnemyPoolObjects[enemyIndex].EnemyPool.Count; j++)
        {
            if (!EnemyPoolObjects[enemyIndex].EnemyPool[j].activeInHierarchy)
            {
                return EnemyPoolObjects[enemyIndex].EnemyPool[j];
            }
        }
        return null;
    }
    public void DeactivateEnemy(GameObject enemy)
    {
        if(enemy != null && enemy.activeInHierarchy)
        {
            enemy.SetActive(false);
        }
    }
}