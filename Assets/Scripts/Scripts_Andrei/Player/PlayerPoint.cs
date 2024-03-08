using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public GameObject Self;
    public int Health = 100;
    public bool PlayerGettingAttacked = false;
    public static PlayerPoint Instance { get; private set; }
    private void Awake()
    {
        Instance = GetComponent<PlayerPoint>();
    }

    public void PlayerTakesDamage(int _damage)
    {
        Health -= _damage;
        PlayerGettingAttacked = true;
        Debug.Log($"Current Player HP {Health}");
        if(Health <= 0)
        {
            Die(Self);
        }
    }

    public void Die(GameObject _self)
    {
        Debug.Log("Player Died!");
        ResetHealth();
    }
    void ResetHealth()
    {
        PlayerGettingAttacked=false;
    }

}
