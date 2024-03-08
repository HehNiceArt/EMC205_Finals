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
    private bool _isOvershooting;
    private Rigidbody _rigidBody;

    public static EnemyAgent Instance { get; private set; }
    private void Awake()
    {
        Instance = GetComponent<EnemyAgent>();
    }
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _speed = Stats.MovementSpeed;
        _angularSpeed = Stats.AngularSpeed;
        _acceleration = Stats.Acceleration;
        _isOvershooting = Stats.IsOvershooting;
        EnemyParameters();
        CheckProximity();
        DetectTree();
        if (_isOvershooting == true) { Agent.velocity = Agent.desiredVelocity; }
        else { _isOvershooting = false; }
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

    public void CheckProximity()
    {
        float _distanceToTree = Vector3.Distance(transform.position, TreePoint.Instance.Self.transform.position);
        if(_distanceToTree < StoppingDistance)
        {
            StopAgent();
            _rigidBody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else { _rigidBody.constraints = RigidbodyConstraints.None; }
    }
    public void DetectTree()
    {
        float _distance = Vector3.Distance(transform.position, TreePoint.Instance.Self.transform.position);
        if(_distance < TreePoint.Instance._detectionRange)
        {
            TreePoint.Instance.DetectEnemies = true;
            Debug.Log($"Detect Tree {TreePoint.Instance.DetectEnemies}");
        }
        else
        {
            TreePoint.Instance.DetectEnemies= false;
            Debug.Log($"Detect Tree {TreePoint.Instance.DetectEnemies}");
        }
    }
}
