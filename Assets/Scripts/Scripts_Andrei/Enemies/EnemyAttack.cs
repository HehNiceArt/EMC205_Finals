using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody _enemyRigidbody;
    [SerializeField] private GameObject _enemyHead;

    [Header("Scripts")]
    public EnemyAgent EnemyNavMeshAgent;
    public EnemyController EnemyController;
    public TreeGrow Tree;


    [Header("Enemy Attack")]
    [SerializeField] private float _enemyAttackSpeed;
    [SerializeField] private float _enemyAttackRange;

    [Space(20f)]

    [SerializeField] private bool _isWithinRange;
    [SerializeField] private bool _isAttacking;

    void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        AttackRange();
        EnemyDestination();
        CheckProximity();
    }

    void AttackRange()
    {

    }
    void EnemyDestination() => EnemyNavMeshAgent.Agent.destination = Tree.transform.position;
    void StopAgent()
    {
        EnemyNavMeshAgent.Agent.isStopped = true;
        print("Stop!");
    }

    void CheckProximity()
    {
        float distanceToTarget = Vector3.Distance(transform.position, Tree.TreeScale.transform.position);
        if (distanceToTarget < EnemyNavMeshAgent.StoppingDistance) 
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
    }
}
