using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public PlayerItems playerItems;
    public float attackRange = 1.5f; // Adjust as needed
    public float attackSpeed = 1.0f; // Adjust as needed
    public int damageAmount = 20; // Adjust as needed

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    StartCoroutine(PerformAttack());
                    break; // Stop checking for enemies after initiating attack
                }
            }
        }
        else
        {
            animator.SetBool("attacking", false);
        }
    }

    IEnumerator PerformAttack()
    {
        animator.SetBool("attacking", true);

        yield return new WaitForSeconds(0.5f);

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRange, transform.forward);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyDamage enemyDamage = hit.collider.GetComponent<EnemyDamage>();
                if (enemyDamage != null)
                {
                    enemyDamage.InflictDamage(damageAmount);
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("attacking", false);

        // Cooldown before next attack can be initiated
        yield return new WaitForSeconds(1 / attackSpeed);
    }
}
