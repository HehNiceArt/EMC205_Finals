using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    // layer mask only contains tree layer
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _damage;
    [Header("Player Items")]
    [SerializeField] private PlayerItems[] _items;
    [Space(10)]
    public bool IsPitchfork = false;
    public bool IsSlingshot = false;
    Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (IsPitchfork) { _raycastDistance = _items[0].ItemRange; } //Pitchfork
        if (IsSlingshot) { _raycastDistance = _items[1].ItemRange; } //Slingshot

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, _raycastDistance, _layerMask))
        {
            if(hit.collider.CompareTag("Enemy") && Input.GetMouseButtonDown(0))
            {
                GameObject _hitEnemy = hit.collider.gameObject;
                if (IsPitchfork)
                {
                    Pitchfork(_hitEnemy);
                }
            }
        }
    }

    void Pitchfork(GameObject _hit)
    {
        _damage = _items[0].ItemAttackDamage;
        EnemyHealth _enemyHP = _hit.GetComponent<EnemyHealth>();
        _enemyHP.TakeDamage(_damage);
    }
}
