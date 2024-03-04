using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Items", menuName ="ScriptableObject/Player Items", order = 1)]
public class PlayerItems : ScriptableObject
{
    public string ItemName;
    public int ItemID;

    public GameObject ItemPrefab;
    public Texture Icon;

    [Header("Quantity")]
    public int MaxQuantity;

    [Header("Attack Stats")]    
    public int ItemAttackDamage;
    public int ItemRange;
    public int ItemAttackSpeed;
}
