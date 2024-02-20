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
    public EnemyAgent[] EnemyAgent;
    public ScriptableObject[] EnemyScriptables;

    [Header("Tree")]
    public GameObject Tree;

    [Header("AI")]
    [SerializeField] private GameObject _attackPoint;
    [SerializeField] private float _minimumRange;

    public NavMeshSurface NavSurface;


    private bool _isTimerRunning;
    private float _startTime = 10f;
    private float _currentTime;
    private void Start()
    {
        _currentTime = _startTime;
        _isTimerRunning = true;
        NavmeshSurfaceUpdate();
    }

    private void Update()
    {

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
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0f)
        {
            _currentTime = 0f;
            _isTimerRunning = false;

            NavmeshSurfaceUpdate();
        }
    }
    void NavMeshResetTimer()
    {
        _currentTime = _startTime;
        _isTimerRunning = true;
    }
    #endregion
    void NavmeshSurfaceUpdate()
    {
        //After the discussion whether the enemy spawns per wave or not
        //implement this
        NavSurface.BuildNavMesh();
    }

}
