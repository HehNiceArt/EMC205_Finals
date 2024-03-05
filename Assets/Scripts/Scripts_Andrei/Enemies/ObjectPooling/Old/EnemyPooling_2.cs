using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling_2 : MonoBehaviour
{
    public static EnemyPooling_2 Instance;

    public List<GameObject> EnemyPrefab;
    public int _initialPoolSize = 1;
    public List<Transform> SpawnPoints;
    public float TimeBetweenWaves = 10f;
    public float SpawnRate = 5f;

    private List<GameObject> _activeEnemies = new List<GameObject>();
    private int _currentWave = 0;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        InitializeObjectPool();
        StartCoroutine(SpawnWaves());
        
    }
    void InitializeObjectPool()
    {
        foreach(var prefab in EnemyPrefab)
        {
            for(int i = 0; i < _initialPoolSize; i++)
            {
                GameObject _enemy = Instantiate(prefab, transform);
                _enemy.SetActive(false);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeBetweenWaves);
            _currentWave++;
            Debug.Log($"Current Wave {_currentWave}");
            int _enemiesToSpawn = _currentWave * 2;
            Debug.Log($"Enemy Count: {_enemiesToSpawn}");
            for(int i = 0; i < _enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(SpawnRate);
            }
            yield return new WaitUntil(() => _activeEnemies.Count == 0);
        }
    }
    void SpawnEnemy()
    {
        GameObject _enemy = GetPooledEnemy();
        if( _enemy != null )
        {
            Transform _spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
            _enemy.transform.position = _spawnPoint.position;
            _enemy.SetActive(true);
            _activeEnemies.Add( _enemy );
        }
    }

    public void ReturnPool(GameObject enemy)
    {
        enemy.SetActive(false);
        _activeEnemies.Remove(enemy);
    }
    GameObject GetPooledEnemy()
    {
        foreach (var prefab in EnemyPrefab)
        {
            Transform _prefabTransform = prefab.transform;
            GameObject _enemy = transform.Find(_prefabTransform.name + "(Clone)").gameObject;

            if(_enemy == null)
            {
                _enemy = Instantiate(prefab, transform);
                _enemy.SetActive(false);
            }
            else if (!_enemy.activeInHierarchy) { return _enemy; }
        }
        return null;
    }

}
