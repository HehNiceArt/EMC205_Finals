using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToTree : MonoBehaviour
{
    EnemyAttack _attack;
    private void Awake()
    {
        _attack = GetComponent<EnemyAttack>();
    }
    public void EnemyAttackPlayer(int _damage)
    {
        //Debug.Log($"Attacking Player {_attack.IsAttackingPlayer}");
        PlayerPoint.Instance.PlayerTakesDamage(_damage);
    }
    public void EnemyAttackTree(int _damage)
    {
        //Debug.Log($"Attacking Tree {_attack.IsAttackingTree}");
        
        TreePoint.Instance.EnemyAttackTree(_damage);
    }
}
