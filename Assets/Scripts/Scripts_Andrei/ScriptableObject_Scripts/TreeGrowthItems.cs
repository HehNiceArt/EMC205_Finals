using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Quirante
[CreateAssetMenu(fileName = "Tree Grow Items", menuName = "ScriptableObject/Tree Growth Items", order = 1)]
public class TreeGrowthItems : ScriptableObject
{
    public string ItemName;
    public int ItemID;

    [Space(10)]
    public GameObject ItemPrefab;
    public Texture Icon;

    [Header("Quantity")]
    public int MaxQuantity;

    [Tooltip("To increase the scale of the tree")]
    [Header("Item Value")]
    public int ItemValue;
}
