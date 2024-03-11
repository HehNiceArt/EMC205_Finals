using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health = 100; // Initial health of the enemy
    public EnemyStats EnemyStats;

    public void InflictDamage(int damageAmount)
    {
        health -= damageAmount; // Decrease the enemy's health
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the enemy if health drops to or below 0
        }
    }
}