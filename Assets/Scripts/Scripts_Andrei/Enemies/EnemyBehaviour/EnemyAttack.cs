using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody _enemyRigidbody;
    [SerializeField] private GameObject _enemyHead;
    public EnemyStats EnemyStats;

    [Header("Scripts")]
    public EnemyAgent EnemyNavMeshAgent;

    [Header("Enemy Attack")]
    [SerializeField] private float _enemyAttackRange;
    [SerializeField] private float _enemyAttackDistance;

    [Space(20f)]

    //Makes sure to follow the tree from anywhere
    [SerializeField] private bool _isFollowingTree = true;
    [SerializeField] private bool _isGettingAttacked = false;
    [SerializeField] private bool _isAttacking = false;

    float _distanceToTarget;
    void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
       _distanceToTarget = Vector3.Distance(transform.position, TreePoint.Instance.transform.position); 
        WithinTreeRange();
        CheckProximity();
        EnemyGetsAttacked();
        EnemyBehaviour();
    }
    void StopAgent()
    {
        EnemyNavMeshAgent.Agent.isStopped = true;
    }
    void WithinTreeRange()
    {
        if (_isFollowingTree)
        {
            EnemyAttacksTree();
        }
    }

    void EnemyGetsAttacked()
    {
        //If enemy is damaged by the player & 
        //Enemy's distance is far away from the attack range
        if (EnemyHealth.Instance.Health < EnemyHealth.Instance.EnemyStats.Health )
        {
            _isFollowingTree = false;
            _isGettingAttacked = true;
        }
        else
        {
            _isGettingAttacked = false;
        }
    }
    void EnemyBehaviour()
    {
        //If the enemy is far from the tree & gets attacked by the player
        //The enemy will attack the player instead
        if(_distanceToTarget > _enemyAttackRange && _isGettingAttacked == true)
        {
            Debug.Log($"Enemy follows player {_isGettingAttacked}");
            EnemyAgent.Instance.FacePlayer();
            EnemyAttacksPlayer();
            StartCoroutine(Attack());
        }
        else
        {
            Debug.Log($"Enemy follows tree {!_isGettingAttacked}");
            EnemyAgent.Instance.FaceTree();
            EnemyAttacksTree();
            if(_distanceToTarget < _enemyAttackDistance)
            {
                Debug.Log("Start Attack");
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(EnemyStats.AttackCooldown);
        Debug.Log($"Attacking! Attacking Cooldown{EnemyStats.AttackCooldown}");
    }
    #region Which to follow?
    void EnemyAttacksTree() => EnemyNavMeshAgent.Agent.destination = TreePoint.Instance.Self.transform.position;
    void EnemyAttacksPlayer() => EnemyNavMeshAgent.Agent.destination = PlayerPoint.Instance.Self.transform.position;
    #endregion
    /// <summary>
    /// Checks the current distance of the enemy to the tree
    /// </summary>
    void CheckProximity()
    {
        _distanceToTarget = Vector3.Distance(transform.position, TreePoint.Instance.Self.transform.position);
        if (_distanceToTarget < EnemyNavMeshAgent.StoppingDistance) 
        { 
            StopAgent();
            _enemyRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else { _enemyRigidbody.constraints = RigidbodyConstraints.None; }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemyAttackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemyAttackDistance);
    }

} 