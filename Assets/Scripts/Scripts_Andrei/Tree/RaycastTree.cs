using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTree : MonoBehaviour
{

    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _playerPosition;


    private void Update()
    {
        RaycastTreeDetection();
    }
    private void RaycastTreeDetection()
    {
        RaycastHit _hit;
        Ray _ray = new Ray(transform.position, transform.forward);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(_ray, out _hit, _raycastDistance, _layerMask))
            {
                TreeGrow._Instance.GrowTreeScale();
                Debug.Log(_hit.collider.gameObject.name);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _raycastDistance);
    }
}
