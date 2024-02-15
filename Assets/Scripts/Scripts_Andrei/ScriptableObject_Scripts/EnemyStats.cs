using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "ScriptableObject/Enemy Stats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public string EnemyName;
    public int CurrentHealth;
    public int MaxHealth;
    public float AttackDamage;
    public float AttackSpeed;
    public int EnemyID;
}
