using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStats EnemyStats;
    public int Health = 0;
    public GameObject Self;

    private int _initialHealth;
    private void Awake()
    {
        _initialHealth = EnemyStats.Health;
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"Enemy {EnemyStats.EnemyName} took {damage} damage.");
        Debug.Log($"Current {EnemyStats.EnemyName} Health: {Health}");
        if(Health <= 0 )
        {
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
        Health = _initialHealth;
    }
}
