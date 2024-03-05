using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public string Name;
    public GameObject EnemyPrefab;
    [Tooltip("The amount of enemies that will spawn")]
    public int PoolSize;
    public float SpawnRatePerEnemy;
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
    [Tooltip("Parent of all the enemies")]
    public GameObject EnemySpawn;

    [Header("Spawn Parameters")]
    public float SpawnRate;

    [Header("Enemy Pool")]
    [SerializeField]
    private List<EnemyPoolList> EnemyPoolObjects = new List<EnemyPoolList>();

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
        StartCoroutine(SpawnEnemies());
    }

    public void StartNextWave()
    {
        StopCoroutine(SpawnEnemies());
        DeactivateAllEnemies();
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            if(AllEnemiesDefeated())
            {
                SpawnEnemiesRandomly();
            }
        }
    }
    void SpawnEnemiesRandomly()
    {
        foreach(var enemyType in EnemyTypes)
        {
            StartCoroutine(SpawnEnemiesOfType(enemyType));
        }
    }

    IEnumerator SpawnEnemiesOfType(EnemyType enemyType)
    {
        int _enemiesToSpawn = enemyType.PoolSize;

        for(int i = 0; i < _enemiesToSpawn; i++)
        {
            GameObject _temp = GetPooledEnemies(EnemyTypes.IndexOf(enemyType));

            if(_temp != null )
            {
                int _rand = Random.Range(0, SpawnAreas.Length);
                Transform _spawnPosition = SpawnAreas[_rand];

                float _spawnDelay = i * enemyType.SpawnRatePerEnemy;

                yield return new WaitForSeconds(_spawnDelay);

                _temp.transform.position = _spawnPosition.position;
                _temp.SetActive(true);
                _temp.transform.parent = EnemySpawn.transform;
            }
        }
    }

    //Checks if all enemies are defeated in the game 
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
            }
            if (!_allDefeated) { break; }
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
    void DeactivateAllEnemies()
    {
        foreach (var poolList in EnemyPoolObjects)
        {
            foreach (var enemy in poolList.EnemyPool)
            {
                if (enemy.activeInHierarchy) { DeactivateEnemy(enemy); }
            }
        }
    }
    public void DeactivateEnemy(GameObject enemy)
    {
        if(enemy != null && enemy.activeInHierarchy) { enemy.SetActive(false); }
    }
}