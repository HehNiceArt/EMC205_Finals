using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Quirante
public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance { get; private set; }
    public EnemyStats EnemyStats;
    public int Health = 0;
    public GameObject Self;
    public bool _isGettingAttacked = false;

    [HideInInspector]
    public int _initialHealth;
    private void Awake()
    {
        Instance = GetComponent<EnemyHealth>();
        _initialHealth = EnemyStats.Health;
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        _isGettingAttacked = true;
        Debug.Log($"Current {EnemyStats.EnemyName} Health: {Health}");
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
