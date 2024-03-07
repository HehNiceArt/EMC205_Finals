using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "ScriptableObject/Enemy Stats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public string EnemyName;
    public int EnemyID;

    [Header("Health")]
    public int Health;

    [Tooltip("To reduce the scale of the tree")]
    [Header("Attack Stats")]
    public float AttackDamage;
    [Tooltip("Next attack time of the enemy")]
    public float AttackTime;
    [Header("Movement")]
    public float MovementSpeed;
}
