using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Runtime.CompilerServices;

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
    [SerializeField] private bool _isAttackingPlayer = false;
    [SerializeField] private bool _isAttackingTree = false;

    float _distanceToTree;
    float _distanceToPlayer;
    float _attackTime;

    EnemyHealth _health;
    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
    }
    void Start()
    {
        _attackTime = EnemyStats.AttackTime;
        _enemyAttackRange = EnemyStats.AttackRange;
        _enemyAttackDistance = EnemyStats.AttackDistance;
        _enemyRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _distanceToTree = Vector3.Distance(transform.position, TreePoint.Instance.transform.position); 
        _distanceToPlayer = Vector3.Distance(transform.position, PlayerPoint.Instance.transform.position);
        WithinTreeRange();
        CheckProximity();
        EnemyBehaviour();
        EnemyGetsAttacked();
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
        if (_health._isGettingAttacked && _distanceToTree > _enemyAttackRange)
        {
            Debug.Log($"HP: {EnemyHealth.Instance.Health}");
            _isFollowingTree = false;
            _isGettingAttacked = true;
        }
        else if (_distanceToTree < _enemyAttackRange)
        {
            _isGettingAttacked = false;
            _isFollowingTree = true;
        }
    }
    void EnemyBehaviour()
    {
        //If the enemy is far from the tree & gets attacked by the player
        //The enemy will attack the player instead
        if(_distanceToTree > _enemyAttackRange && _isGettingAttacked == true)
        {
            _isFollowingTree = false;
            _isAttackingTree = false;
            Debug.Log($"Enemy follows player {_isGettingAttacked}");
            EnemyAgent.Instance.FacePlayer();
            EnemyAttacksPlayer();
            EnemyAgent.Instance.ResumeAgent();
            AttackPlayerTime();
        }
        else
        {
            _isAttackingPlayer = false;
            _isFollowingTree = true;
            Debug.Log($"Enemy follows tree {!_isGettingAttacked}");
            EnemyAgent.Instance.FaceTree();
            EnemyAttacksTree();
            AttackTreeTime();
        }
        if(_distanceToPlayer > _enemyAttackDistance) { _isAttackingPlayer = false; }
    }

    void AttackTreeTime()
    {
        if (_distanceToTree < _enemyAttackDistance)
        {
            _isAttackingTree = true;
            _attackTime -= Time.deltaTime;
            if (_attackTime <= 0)
            {
                Attack();
                _attackTime = EnemyStats.AttackTime;
            }
        }
    }
    void AttackPlayerTime()
    {
        if (_distanceToPlayer < _enemyAttackDistance)
        {
            _isAttackingPlayer = true;
            _attackTime -= Time.deltaTime;
            if (_attackTime <= 0)
            {
                Attack();
                _attackTime = EnemyStats.AttackTime;
            }
        }
    }

    void Attack()
    {
        if( _isAttackingPlayer )
        {
            Debug.Log($"Attacking Player {_isAttackingPlayer}");
        }
        else if( _isAttackingTree )
        {
            Debug.Log($"Attacking Tree {_isAttackingTree}");
        }
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
        _distanceToTree = Vector3.Distance(transform.position, TreePoint.Instance.Self.transform.position);
        if (_distanceToTree < EnemyNavMeshAgent.StoppingDistance) 
        { 
            EnemyAgent.Instance.StopAgent();
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

    private void OnDisable()
    {
        _isFollowingTree = true;
        _isGettingAttacked = false;
        _isAttackingPlayer = false;
        _isAttackingTree = false;
    }
} 