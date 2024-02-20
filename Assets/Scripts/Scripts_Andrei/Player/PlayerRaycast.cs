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
    public TreeGrow TreeScale;
    public EnemyHealth[] Enemy;
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
            if (hit.collider.CompareTag("Tree") && Input.GetKeyDown(KeyCode.E))
            { 
                TreeGrow._Instance.GrowTreeScale();
            }
            if(hit.collider.CompareTag("Enemy") && Input.GetMouseButtonDown(0))
            {
                for(int i = 0; i < Enemy.Length; i++)
                {
                    Enemy[i].TakeDamage(_damage);
                    Debug.Log("enemy hit!");
                }
            }
                Debug.Log(hit.collider.gameObject.name);
        }
    }
}
