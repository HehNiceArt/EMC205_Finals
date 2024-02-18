using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    // layer mask only contains tree layer
    [SerializeField] private LayerMask _layerMask;

    public TreeGrow TreeScale;

    Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, _raycastDistance, _layerMask))
        {
            if (hit.collider.CompareTag("Tree") && Input.GetKey(KeyCode.E))
            { 
                TreeGrow._Instance.GrowTreeScale();
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
}
