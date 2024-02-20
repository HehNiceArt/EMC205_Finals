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
//need to reference the scriptables instead of this 
    [Space(20f)]

    [SerializeField] private bool _isWithinRange;
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
    void EnemyDestination() => EnemyNavMeshAgent.Agent.destination = TreeGrow._Instance.TreeScale.transform.position;
    void StopAgent()
    {
        EnemyNavMeshAgent.Agent.isStopped = true;
    }


    void AttackRange()
    {
        _distanceToTarget = Vector3.Distance(transform.position, TreeGrow._Instance.TreeScale.transform.position);
        if( _distanceToTarget < _enemyAttackRange)
        {
            _isWithinRange = true;
            _isAttacking = true;
            StartCoroutine(Attacking(_isAttacking));
        }
        else if( _distanceToTarget > _enemyAttackRange)
        {
            _isWithinRange = false;
            _isAttacking = false;
        }
    }
   IEnumerator Attacking(bool isAttacking)
    {
        //Debug.Log(isAttacking);
        if(isAttacking == true)
        {
            //DecreaseScale();
            yield return new WaitForSeconds(EnemyStats.AttackSpeed);
        }
    }
    void DecreaseScale()
    {
        Vector3 decrease = new Vector3(EnemyStats.AttackDamage, EnemyStats.AttackDamage, EnemyStats.AttackDamage);
        TreeGrow._Instance.TreeScale.transform.localScale -= Vector3.ClampMagnitude(Vector3.MoveTowards(TreeGrow._Instance.TreeScale.transform.localScale, decrease, Time.deltaTime), 999);
        Debug.Log(EnemyStats.AttackDamage + " " + decrease);
        CheckScale();
    }
    void CheckScale()
    {
        Vector3 _treeScale = TreeGrow._Instance.TreeScale.transform.localScale;
        Vector3 _minimumScale = new Vector3(1, 1, 1);
    }
    void CheckProximity()
    {
        _distanceToTarget = Vector3.Distance(transform.position, TreeGrow._Instance.TreeScale.transform.position);
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
