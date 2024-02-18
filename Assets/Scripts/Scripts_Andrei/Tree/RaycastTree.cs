using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTree : MonoBehaviour
{

    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Camera _playerCamera;

    private Vector3 _collision = Vector3.zero;

    public TreeGrow TreeGrowScale;

    private void Update()
    {
        //RaycastTreeDetection();
    }
    #region deprcated
    private void RaycastTreeDetection()
    {
        Ray _ray = new Ray(transform.position, transform.forward);
        RaycastHit _hit;
        if (Input.GetKey(KeyCode.E))
        {
            //detects tree when hover by crosshair
            if (Physics.Raycast(_ray, out _hit, _raycastDistance, _layerMask))
            {
                TreeGrow._Instance.GrowTreeScale();
                Debug.Log(_hit.collider.gameObject.name);
            }
            //This should detect the enemy
            else if (Physics.Raycast(_ray, out _hit, _raycastDistance, _layerMask))
            {
                Debug.Log(_hit.collider.gameObject.name);
            }
        }

        if(Physics.Raycast(_ray, out _hit,_raycastDistance, _layerMask))
        {
            _collision = _hit.point;
        }
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _raycastDistance);
    }
}
