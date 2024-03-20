using UnityEngine;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour
{
    public PlayerItems playerItems;
    public float attackRange = 1.5f;
    public float attackSpeed = 1.7f;
    public float attackDamage = 25;
    public Camera _cam;
    public LayerMask layerMask;
    public Animator animator;

    private List<EnemyDamage> enemiesInRange = new List<EnemyDamage>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Change to GetMouseButtonDown to play sound only once per click
        {
            animator.SetBool("attacking", true);

            // Play the melee attack sound
            AudioManage.instance.PlayMeleeAttackSound();

            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, attackRange, layerMask))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    EnemyDamage enemy = hit.collider.GetComponent<EnemyDamage>();
                    if (enemy != null && !enemiesInRange.Contains(enemy))
                    {
                        enemiesInRange.Add(enemy);
                        enemy.TakeMeleeDamage(attackDamage);
                        Debug.Log($"Enemy hit! {hit.collider.name}");
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Stop attacking when the mouse button is released
        {
            animator.SetBool("attacking", false);
            enemiesInRange.Clear();
        }
    }
}
