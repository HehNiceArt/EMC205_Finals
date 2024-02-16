using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeGrow : MonoBehaviour
{
    [Header("Tree")]
    public GameObject TreeScale;
    public float TimeToGrow;

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
        TreeScale.transform.localScale = Vector3.MoveTowards(TreeScale.transform.localScale, _fertilizerScaleIncrease / 2, Time.deltaTime * TimeToGrow);
    }
}
