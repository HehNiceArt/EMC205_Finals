using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour
{
    public PlayerItems playerItems;
    public float attackRange = 1.5f;
    public Camera _cam;
    public LayerMask layerMask;
    public Animator animator;

    private List<EnemyDamage> enemiesInRange = new List<EnemyDamage>();

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("attacking", true);

            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, attackRange, layerMask))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    EnemyDamage enemy = hit.collider.GetComponent<EnemyDamage>();
                    if (enemy != null && !enemiesInRange.Contains(enemy))
                    {
                        enemiesInRange.Add(enemy);
                        enemy.InflictDamage(playerItems.ItemRange);
                        Debug.Log($"Enemy hit! {hit.collider.name}");
                    }
                }
            }
        }
        else
        {
            animator.SetBool("attacking", false);
            // Clear the list of enemies in range when not attacking
            enemiesInRange.Clear();
        }
    }
}
