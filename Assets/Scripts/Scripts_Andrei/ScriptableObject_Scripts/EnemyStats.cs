using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "ScriptableObject/Enemy Stats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public string EnemyName;
    public int EnemyID;

    [Header("Health")]
    public int CurrentHealth;
    public int MaxHealth;

    [Tooltip("To reduce the scale of the tree")]
    [Header("Attack Stats")]
    public float AttackDamage;
    public float AttackSpeed;
    [Header("Movement")]
    public float MovementSpeed;
}
