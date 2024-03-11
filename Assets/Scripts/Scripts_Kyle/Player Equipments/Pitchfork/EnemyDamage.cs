using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health = 100; 
    public EnemyStats EnemyStats;

    public void InflictDamage(int damageAmount)
    {
        health -= damageAmount; 
        if (health <= 0)
        {
            Destroy(gameObject); 
        }
    }
}