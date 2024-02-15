using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tree Grow Items", menuName = "ScriptableObject/Tree Growth Items", order = 1)]
public class TreeGrowthItems : ScriptableObject
{
    public string ItemName;
    public float ItemValue;
    public int ItemID;
}
