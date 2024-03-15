using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollidesToEnemy : MonoBehaviour
{
    public LayerMask _enemyLayer;
    public PlayerItems _slingshotVal;

    public GameObject Self;
    int _damage;
    private void Start()
    {
        _damage = _slingshotVal.ItemAttackDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log($"Enemy hit! {collision.gameObject.name}");
            EnemyHealth _enemyHP = collision.collider.GetComponent<EnemyHealth>();
            _enemyHP.TakeDamage(_damage);
            Self.SetActive(false);
        }
        if (collision.gameObject.tag == "Floor")
        {
            Self.SetActive(false);
        }
    }
}
