using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [HideInInspector] public Animator Anim;
    EnemyAgent _agent;
    EnemyToTree _toTree;
    EnemyStats _stats;
    EnemyHealth _health;
    EnemyAttack _attack;

    int _enemyDMG;
    void Start()
    {
        Anim = GetComponent<Animator>();    
        _agent = GetComponentInParent<EnemyAgent>();
        _toTree = GetComponentInParent<EnemyToTree>();
        _health = GetComponentInParent<EnemyHealth>();
        _attack = GetComponentInParent<EnemyAttack>();
        _stats = _agent.Stats;
        _enemyDMG = _stats.AttackDamage;  
    }

    public void PlayerGetsAttacked()
    {
        _toTree.EnemyAttackPlayer(_stats.AttackDamage);
    }
    public void TreeGetsAttacked() => _toTree.EnemyAttackTree(_stats.AttackDamage);
    public void EnemyIsDead()
    {
        Debug.Log("Enemy is dead!");
        _health.Die(gameObject.transform.parent.gameObject);
        _agent.Stats.AttackDamage = _enemyDMG;
    }
    public void StopAttacking()
    {
        _agent._rigidBody.constraints = RigidbodyConstraints.FreezePosition;
        _health._isGettingAttacked = false;
        _attack.IsAttackingPlayer = false;
        _attack.IsAttackingTree = false;
        _attack._enemyStats.AttackDamage = 0;
    }
}
