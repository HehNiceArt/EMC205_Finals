using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public PlayerItems playerItems;
    public float attackRange = 1.5f;
    public float attackSpeed = 1.0f; // Adjust as needed
    public int damageAmount = 20; // Adjust as needed
    public Camera _cam;
    public LayerMask layerMask;
    public EnemyDamage _enemyHP;
   
    private void Update()
    {

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, attackRange, layerMask))
        {
            if (hit.collider.CompareTag("Enemy") && Input.GetMouseButtonDown(0))
            {
                GameObject _hitEnemy = hit.collider.gameObject;
                _enemyHP.InflictDamage(playerItems.ItemRange);
                Debug.Log($"Enemy hit! {hit.collider.name}");
            }
        }
    }
}