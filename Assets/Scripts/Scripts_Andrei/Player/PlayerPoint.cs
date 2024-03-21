using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public GameObject Self;
    public Camera Player;
    public Camera Tree;

    public int MaxHP = 100;
    public int CurrentHP;
    public int RegenPerSecond;
    public bool PlayerGettingAttacked = false;
    public HealthBar HealthBar;
    Rigidbody _rb;
    PlayerMovement _move;
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
        _rb = GetComponent<Rigidbody>();
        _move = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        IfPlayerIsDead();
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

    void IfPlayerIsDead()
    {
        if(CurrentHP <= 0)
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            Player.gameObject.SetActive(false);
            Tree.gameObject.SetActive(true);
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.None;
            Tree.gameObject.SetActive(false);
            Player.gameObject.SetActive(true);
        }
    }
}
