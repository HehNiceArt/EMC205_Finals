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
    public EnemyHealth[] EnemyHealth;


    [Header("AI")]
    [SerializeField] private float _minimumRange;

    public NavMeshSurface NavSurface;

    private void Start()
    {
    }

    private void Update()
    {
    }

}
