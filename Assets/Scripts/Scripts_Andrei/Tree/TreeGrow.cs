using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeGrow : MonoBehaviour
{
    [SerializeField] private GameObject _treeScale;
    //public float fertilizer;
    public float timeToGrow;

    [Header("Scriptable Object")]
    public TreeGrowthItems[] TreeGrowthItems;
    //Singleton
    private static TreeGrow _instance;
    public static TreeGrow _Instance { get { return _instance; } }
    private void Awake()
    {
        if( _instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GrowTreeScale()
    {
        float _fertilizer = TreeGrowthItems[0].ItemValue;
        Vector3 _fertilizerScaleIncrease = new Vector3(_fertilizer, _fertilizer, _fertilizer);
        _treeScale.transform.localScale = Vector3.MoveTowards(_treeScale.transform.localScale, _fertilizerScaleIncrease, timeToGrow);
        Debug.Log("Grow");
    }
}
