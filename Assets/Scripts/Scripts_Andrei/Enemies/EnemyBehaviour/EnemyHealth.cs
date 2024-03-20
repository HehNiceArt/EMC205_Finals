using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Quirante
public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance { get; private set; }
    EnemyStats _enemyStats;
    [HideInInspector] public int Health = 0;
    public GameObject Self;
    [HideInInspector] public bool _isGettingAttacked = false;

    [HideInInspector]
    public int _initialHealth;
    EnemyAgent _enemyAgent;
    private void Awake()
    {
        Instance = GetComponent<EnemyHealth>();
        _enemyAgent = GetComponent<EnemyAgent>();
        _enemyStats = _enemyAgent.Stats;
        _initialHealth = _enemyStats.Health;
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        _isGettingAttacked = true;
        Debug.Log($"Current {_enemyStats.EnemyName} Health: {Health}");
        if (Health <= 0 )
        {
            TreePoint.Instance.DetectEnemies = false;
            Die(Self);
        }
    }

    public void Die(GameObject enemy)
    {
        if(EnemyPoolManager.Instance != null)
        {
            EnemyPoolManager.Instance.DeactivateEnemy(enemy);
            ResetHealth();
        }
    }

    void ResetHealth()
    {
        _isGettingAttacked = false; 
        Health = _initialHealth;
    }
}
