using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyController : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject[] _enemies;

    [Header("Tree")]
    [SerializeField] private GameObject _tree;

    [Header("AI")]
    [SerializeField] private GameObject _attackPoint;
    [SerializeField] private float _minimumRange;

    public NavMeshAgent[] Agent;
    public NavMeshSurface NavSurface;


    private bool _isTimerRunning;
    private float _startTime = 10f;
     private float _currenTime;
    private void Start()
    {
        _currenTime = _startTime;
        _isTimerRunning = true;
        NavmeshSurfaceUpdate();
    }

    private void Update()
    {
        FollowTree();

        #region Timer
        //if (_isTimerRunning)
        //{
        //    NavMeshTimer();
        //}
        #endregion
    }

    #region Timer Logic
    /// <summary>
    /// Timer is set at 60 seconds
    /// Every 60 seconds, the NavMeshSurfaceUpdate gets triggered
    /// NavMeshSurface updates
    /// </summary>
    void NavMeshTimer()
    {
        _currenTime -= Time.deltaTime;

        if(_currenTime <= 0f)
        {
            _currenTime = 0f;
            _isTimerRunning = false;

            NavmeshSurfaceUpdate();
        }
    }
    void NavMeshResetTimer()
    {
        _currenTime = _startTime;
        _isTimerRunning = true;
    }
    #endregion
    void NavmeshSurfaceUpdate()
    {
        //After the discussion whether the enemy spawns per wave or not
        //implement this
        NavSurface.BuildNavMesh();
    }

    void FollowTree()
    {
        foreach (var enemy in _enemies)
        {
            EnemiesFollow();
        }
    }

    void EnemiesFollow()
    {
        foreach (var agent in Agent) 
        {
            agent.destination = _tree.transform.position;
        }
    }
}
