using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Quirante
[System.Serializable]
public class ItemType
{
    public string Name;
    public GameObject ItemPrefab;
    public int PoolSize;
    public float SpawnRate;
}
[System.Serializable]
public class ItemPool
{
    public List<GameObject> Items = new List<GameObject>();
}
public class SpawnItem : MonoBehaviour
{
    public List<ItemType> Items = new List<ItemType>();
    private List<ItemPool> ItemPool = new List<ItemPool>();
    public GameObject ItemParent;
    public Transform[] SpawnPoint;
    public float SpawnRate;
    public static SpawnItem Instance;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }
    private void Start()
    {
        InitializePool();
        StartWave();
    }

    void InitializePool()
    {
        foreach( var  item in Items )
        {
            List<GameObject> _itemPool = new List<GameObject> ();
            for (int i = 0; i < item.PoolSize; i++)
            {
                GameObject _item = Instantiate(item.ItemPrefab);
                _item.SetActive(false);
                _itemPool.Add(_item);
            }
            ItemPool _itemPoolList = new ItemPool { Items = _itemPool };
            ItemPool.Add(_itemPoolList);
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnItems());
    }
    public void StartNextWave()
    {
        StopCoroutine(SpawnItems());
        DeactivateAllItems();
        StartCoroutine(SpawnItems());
    }
    IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            if (AllItemsTaken())
            {
                SpawnItemsRandomly();
            }
        }
    }
    void SpawnItemsRandomly()
    {
        foreach ( var item in Items)
        {
            StartCoroutine(SpawnItemsOfType(item));
        }
    }
    IEnumerator SpawnItemsOfType(ItemType items)
    {
        int _itemsToSpawn = items.PoolSize;
        for(int i = 0; i < _itemsToSpawn; i++)
        {
            GameObject _temp = GetPooledItems(Items.IndexOf(items));

            if( _temp != null )
            {
                int _rand = Random.Range(0, SpawnPoint.Length);
                Transform _spawnPosition = SpawnPoint[_rand];
                float _spawnDelay = i * items.SpawnRate;
                yield return new WaitForSeconds(_spawnDelay);
                _temp.transform.position = _spawnPosition.position;
                _temp.SetActive(true);
                _temp.transform.parent = ItemParent.transform;
            }
        }
    }

    public GameObject GetPooledItems(int itemIndex)
    {
        for(int i = 0; i < ItemPool[itemIndex].Items.Count; i++)
        {
            if (!ItemPool[itemIndex].Items[i].activeInHierarchy)
            {
                return ItemPool[itemIndex].Items[i];
            }
        }
        return null;
    }
    bool AllItemsTaken()
    {
        bool _allTaken = true;
        foreach(var itemPool in ItemPool)
        {
            foreach(var item in itemPool.Items)
            {
                if (item.activeInHierarchy)
                {
                    _allTaken = false;
                    break;
                }
            }
            if(!_allTaken) { break; }
        }
        return _allTaken;
    }

    void DeactivateAllItems()
    {
        foreach(var poolList in ItemPool)
        {
            foreach (var item in poolList.Items)
            {
                if (item.activeInHierarchy) { DeactivateItem(item); }
            }
        }
    }
    public void DeactivateItem(GameObject item)
    {
       if(item != null && item.activeInHierarchy) { item.SetActive(false); } 
    }
}
