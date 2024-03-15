using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public GameObject Self;
    public int MaxHP = 100;
    public int CurrentHP;
    public int RegenPerSecond;
    public bool PlayerGettingAttacked = false;
    public HealthBar HealthBar;
    public static PlayerPoint Instance { get; private set; }
    private void Awake()
    {
        Instance = GetComponent<PlayerPoint>();
    }
    private void Start()
    {
        CurrentHP = MaxHP;
        HealthBar.SetHealth(MaxHP);
        StartCoroutine(Regen());
    }

    public void PlayerTakesDamage(int _damage)
    {
        CurrentHP -= _damage;
        PlayerGettingAttacked = true;
        HealthBar.SetHealth(CurrentHP);
        Debug.Log($"Current Player HP {CurrentHP}");
        if(CurrentHP <= 0)
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

    IEnumerator Regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            CurrentHP = Mathf.Min(CurrentHP + RegenPerSecond, MaxHP);
            HealthBar.SetHealth(CurrentHP);
        }

    }

}
