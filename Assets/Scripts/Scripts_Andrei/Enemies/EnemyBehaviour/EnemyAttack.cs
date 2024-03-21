using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

//Andrei Quirante
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _enemyHead;

    [Header("Scripts")]
    public EnemyAgent EnemyNavMeshAgent;

    private float _enemyAttackRange;
    private float _enemyAttackDistance;

    [Space(20f)]

    //Makes sure to follow the tree from anywhere
    private bool _isFollowingTree = true;
    private bool _isGettingAttacked = false;
    [HideInInspector]
    public bool IsAttackingPlayer = false;
    //[HideInInspector]
    public bool IsAttackingTree = false;

    float _distanceToTree;
    float _distanceToPlayer;
    float _attackTime;

    [HideInInspector]public EnemyStats _enemyStats;
    EnemyHealth _health;
    EnemyToTree _enemyDMG;
    EnemyAgent _enemyAgent;
    EnemyAnimations _anim;
    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _enemyDMG = GetComponent<EnemyToTree>();
        _enemyAgent = GetComponent<EnemyAgent>();
        _anim = GetComponentInChildren<EnemyAnimations>();
        _enemyStats = _enemyAgent.Stats;
    }
    void Start()
    {
        _attackTime = _enemyStats.AttackTime;
        _enemyAttackRange = _enemyStats.AttackRange;
        _enemyAttackDistance = _enemyStats.AttackDistance;
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
            //Debug.Log($"Enemy follows player {_isGettingAttacked}");
            EnemyAgent.Instance.FacePlayer();
            EnemyAttacksPlayer();
            EnemyAgent.Instance.ResumeAgent();
            AttackPlayerTime();
        }
        else
        {
            IsAttackingPlayer = false;
            _isFollowingTree = true;
            //Debug.Log($"Enemy follows tree {_isFollowingTree}");
            EnemyAgent.Instance.FaceTree();
            EnemyAttacksTree();
            AttackTreeTime();
        }
        if(_distanceToPlayer > _enemyAttackDistance) { IsAttackingPlayer = false; }
    }

    int _count = 0;
    void AttackTreeTime()
    {
        if (_distanceToTree < _enemyAttackDistance)
        {
            _anim.Anim.SetBool("IsAttackingTree", true);
            IsAttackingTree = true;
            _attackTime -= Time.deltaTime;
            if (_attackTime <= 0)
            {
                _attackTime = _enemyStats.AttackTime;
                _count += 1;
                Perish(_count); 
            }
        }
    }
    void Perish(int _count)
    {
        int _currentCount = _enemyStats.AttackCount;
        if(_count > _currentCount)
        {
            _health.TakeDamage(999); 
        }
    }
    void AttackPlayerTime()
    {
        if (_distanceToPlayer < _enemyAttackDistance)
        {
            _anim.Anim.SetBool("IsAttackingPlayer", true);
            IsAttackingPlayer = true;
            _attackTime -= Time.deltaTime;
            if (_attackTime <= 0)
            {
                _attackTime = _enemyStats.AttackTime;
            }
        }
        else { _anim.Anim.SetBool("IsAttackingPlayer", false); }
    }
    #region Which to follow?
    void EnemyAttacksTree() => EnemyNavMeshAgent.Agent.destination = TreePoint.Instance.Self.transform.position;
    void EnemyAttacksPlayer() => EnemyNavMeshAgent.Agent.destination = PlayerPoint.Instance.Self.transform.position;
    
    #endregion
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
        _enemyAgent._rigidBody.constraints = RigidbodyConstraints.None;
    }
} 