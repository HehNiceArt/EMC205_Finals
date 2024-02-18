using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody _enemyRigidbody;
    public NavMeshAgent EnemyControl;
    [SerializeField] private float _enemyMoveSpeed;

    [Header("Enemy Attack")]
    [SerializeField] private float _enemyAttackSpeed;
    [SerializeField] private float _enemyAttackRange;
    [Space(20f)]
    [SerializeField] private bool _isWithinRange;
    [SerializeField] private bool _isAttacking;

    void Start()
    {
        EnemyControl = GetComponent<NavMeshAgent>();
        _enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemyAttackRange);
    }
}
