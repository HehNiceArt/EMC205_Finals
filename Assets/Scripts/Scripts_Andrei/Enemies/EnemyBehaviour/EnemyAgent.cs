using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour
{
    public NavMeshAgent Agent;
    public EnemyStats Stats;
    [Header("Enemy Parameters")]
    private float _speed;
    private float _angularSpeed;
    private float _acceleration;
    public float StoppingDistance;

    [Space(20f)]
    [Tooltip("If the enemy is overshooting the tree, tick this!")]
    public bool IsOvershooting = false;

    public static EnemyAgent Instance { get; private set; }
    private void Awake()
    {
        Instance = GetComponent<EnemyAgent>();
    }
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        _speed = Stats.MovementSpeed;
        _angularSpeed = Stats.AngularSpeed;
        _acceleration = Stats.Acceleration;
        EnemyParameters();
        if (IsOvershooting == true) { Agent.velocity = Agent.desiredVelocity; }
        else { IsOvershooting = false; }
    }
    
    public void FaceTree()
    {
        Vector3 _direction = (TreePoint.Instance.Self.transform.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5f);
    }
    public void FacePlayer()
    {
        Vector3 _direction = (PlayerPoint.Instance.Self.transform.position - transform.position).normalized;
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
    public void StopAgent() => Agent.isStopped = true;
    public void ResumeAgent() => Agent.isStopped = false;
}
