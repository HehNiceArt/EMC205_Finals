using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager SharedInstance;
    public List<GameObject> EnemyPoolObjects;
    public GameObject GiantAnt;
    public GameObject IllegalCutter;
    public Transform[] SpawnAreas;
    public int AmountToPool;
    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        EnemyPoolObjects = new List<GameObject>();
        GameObject _giantAnt;
        GameObject _illegalCutter;
        
        for(int i = 0; i < AmountToPool; i++)
        {
            _giantAnt = Instantiate(GiantAnt);
            _giantAnt.SetActive(false);
            _illegalCutter = Instantiate(IllegalCutter);
            _illegalCutter.SetActive(false);
            EnemyPoolObjects.Add(_giantAnt);
            EnemyPoolObjects.Add(_illegalCutter);
        }
        StartCoroutine(SpawnEnemiesRandomly());
    }
    IEnumerator SpawnEnemiesRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject _temp = GetPooledObject();
            if (_temp != null)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                Debug.Log(spawnPosition);
                _temp.transform.position = spawnPosition;
                _temp.SetActive(true);
            }
        }
    }
    public GameObject GetPooledObject()
    {
        for(int j = 0; j < AmountToPool; j++)
        {
            if (!EnemyPoolObjects[j].activeInHierarchy)
            {
                return EnemyPoolObjects[j];
            }
        }
        return null;
    }
}