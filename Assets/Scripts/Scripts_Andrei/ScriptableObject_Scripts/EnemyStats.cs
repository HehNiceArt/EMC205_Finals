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
    public int AttackDamage;
    [Tooltip("Next attack time of the enemy")]
    public float AttackTime;
    public float AttackRange;
    public float AttackDistance;
    [Tooltip("This is the number of attacks the enemy could have against the tree")]
    [Range(1, 5)]public int AttackCount;

    [Header("Movement")]
    [Range(0, 10)]
    public float MovementSpeed;
    [Tooltip("Maximum turning speed while following a path")]
    [Range(0, 100)]
    public float AngularSpeed;
    [Tooltip("Maximum acceleration of the enemy")]
    [Range(0, 10)]
    public float Acceleration;
    [Space(10)]
    [Tooltip("If the enemy is overshooting the tree, tick this!")]
    public bool IsOvershooting;
}
