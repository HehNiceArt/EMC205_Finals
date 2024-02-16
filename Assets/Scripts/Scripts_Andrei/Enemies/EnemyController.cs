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
    private void Update()
    {
        FollowTree();
        NavmeshSurfaceUpdate();
    }

    void NavmeshSurfaceUpdate()
    {
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
