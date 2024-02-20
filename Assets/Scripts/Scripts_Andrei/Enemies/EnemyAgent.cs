using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour
{
    public NavMeshAgent Agent;

    [Header("Enemy Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private float _acceleration;
    public float StoppingDistance;

    [Space(20f)]
    [Tooltip("If the enemy is overshooting the tree, tick this!")]
    public bool IsOvershooting = false;
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        FaceTree();
        EnemyParameters();
        if (IsOvershooting == true) { Agent.velocity = Agent.desiredVelocity; }
        else { IsOvershooting = false; }
    }
    
    void FaceTree()
    {
        Vector3 _direction = (TreeGrow._Instance.TreeScale.transform.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5f);
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
