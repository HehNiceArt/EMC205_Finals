using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Runtime.CompilerServices;

public class EnemyAttack : MonoBehaviour
{
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
    public bool IsAttackingPlayer = false;
    public bool IsAttackingTree = false;

    float _distanceToTree;
    float _distanceToPlayer;
    float _attackTime;

    EnemyHealth _health;
    EnemyToTree _enemyDMG;
    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _enemyDMG = GetComponent<EnemyToTree>();
    }
    void Start()
    {
        _attackTime = EnemyStats.AttackTime;
        _enemyAttackRange = EnemyStats.AttackRange;
        _enemyAttackDistance = EnemyStats.AttackDistance;
    }
    void Update()
    {
        _distanceToTree = Vector3.Distance(transform.position, TreePoint.Instance.transform.position); 
        _distanceToPlayer = Vector3.Distance(transform.position, PlayerPoint.Instance.transform.position);
        WithinTreeRange();
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
            IsAttackingTree = false;
            Debug.Log($"Enemy follows player {_isGettingAttacked}");
            EnemyAgent.Instance.FacePlayer();
            EnemyAttacksPlayer();
            EnemyAgent.Instance.ResumeAgent();
            AttackPlayerTime();
        }
        else
        {
            IsAttackingPlayer = false;
            _isFollowingTree = true;
            Debug.Log($"Enemy follows tree {_isFollowingTree}");
            EnemyAgent.Instance.FaceTree();
            EnemyAttacksTree();
            AttackTreeTime();
        }
        if(_distanceToPlayer > _enemyAttackDistance) { IsAttackingPlayer = false; }
    }

    void AttackTreeTime()
    {
        if (_distanceToTree < _enemyAttackDistance)
        {
            IsAttackingTree = true;
            _attackTime -= Time.deltaTime;
            if (_attackTime <= 0)
            {
                _enemyDMG.EnemyAttackTree(EnemyStats.AttackDamage);
                _attackTime = EnemyStats.AttackTime;
            }
        }
    }
    void AttackPlayerTime()
    {
        if (_distanceToPlayer < _enemyAttackDistance)
        {
            IsAttackingPlayer = true;
            _attackTime -= Time.deltaTime;
            if (_attackTime <= 0)
            {
                _enemyDMG.EnemyAttackPlayer(EnemyStats.AttackDamage);
                _attackTime = EnemyStats.AttackTime;
            }
        }
    }
    #region Which to follow?
    void EnemyAttacksTree() => EnemyNavMeshAgent.Agent.destination = TreePoint.Instance.Self.transform.position;
    void EnemyAttacksPlayer() => EnemyNavMeshAgent.Agent.destination = PlayerPoint.Instance.Self.transform.position;
    #endregion
    /// <summary>
    /// Checks the current distance of the enemy to the tree
    /// </summary>
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
        IsAttackingPlayer = false;
        IsAttackingTree = false;
    }
} 