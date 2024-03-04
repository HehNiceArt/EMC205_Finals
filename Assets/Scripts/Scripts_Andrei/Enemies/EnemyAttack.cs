using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody _enemyRigidbody;
    [SerializeField] private GameObject _enemyHead;
    public EnemyStats EnemyStats;

    [Header("Scripts")]
    public EnemyAgent EnemyNavMeshAgent;

    [Header("Enemy Attack")]
    [SerializeField] private float _enemyAttackRange;

    [Space(20f)]

    //[SerializeField] private bool _isWithinRange;
    [SerializeField] private bool _isAttacking;

    float _distanceToTarget;
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
    void EnemyDestination() => EnemyNavMeshAgent.Agent.destination = TreePoint.Instance.Self.transform.position;
    void StopAgent()
    {
        EnemyNavMeshAgent.Agent.isStopped = true;
    }


    void AttackRange()
    {
        _distanceToTarget = Vector3.Distance(transform.position, TreePoint.Instance.Self.transform.position);
        if( _distanceToTarget < _enemyAttackRange)
        {
            //_isWithinRange = true;
            _isAttacking = true;
            StartCoroutine(Attacking(_isAttacking));
        }
        else if( _distanceToTarget > _enemyAttackRange)
        {
            //_isWithinRange = false;
            _isAttacking = false;
        }
    }
   IEnumerator Attacking(bool isAttacking)
    {
        if(isAttacking == true)
        {
            //Debug.Log("Attacking");
            yield return new WaitForSeconds(EnemyStats.AttackSpeed);
        }
    }
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
    }
}
