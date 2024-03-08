using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    // layer mask only contains tree layer
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _damage;
    [SerializeField] private EnemyPoolManager _enemyPoolManager;
    Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, _raycastDistance, _layerMask))
        {
            if(hit.collider.CompareTag("Enemy") && Input.GetMouseButtonDown(0))
            {
                GameObject _hitEnemy = hit.collider.gameObject;
                EnemyHealth _enemyHP = _hitEnemy.GetComponent<EnemyHealth>(); 
                _enemyHP.TakeDamage(_damage);
            }
        }
    }
}
