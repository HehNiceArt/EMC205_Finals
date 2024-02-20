using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour
{
    public NavMeshAgent Agent;
    public TreeGrow Tree;

    [Header("Enemy Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private float _acceleration;
    public float StoppingDistance;

    [Space(20f)]
    public bool IsOvershooting = false;
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        transform.LookAt(Tree.transform.position);
        EnemyParameters();
        //Agent.velocity = Agent.desiredVelocity;
        if (IsOvershooting == true) { Agent.velocity = Agent.desiredVelocity; }
        else { IsOvershooting = false; }
    }
    
    void EnemyParameters()
    {
        Agent.speed = _speed;
        Agent.angularSpeed = _angularSpeed;
        Agent.acceleration = _acceleration;
        Agent.stoppingDistance = StoppingDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, StoppingDistance);
    }
}
