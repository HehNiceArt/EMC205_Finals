using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStats EnemyStats;

    private void Start()
    {
        EnemyStats.CurrentHealth = EnemyStats.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        EnemyStats.CurrentHealth -= damage;

        Debug.Log(EnemyStats.CurrentHealth);
        if(EnemyStats.CurrentHealth <= 0 )
        {
            EnemyStats.CurrentHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
