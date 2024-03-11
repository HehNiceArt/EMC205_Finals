using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int regenAmountPerSecond = 5; // Amount of health regenerated per second
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);

        // Start the coroutine for passive health regeneration
        StartCoroutine(RegenerateHealth());
    }

    
    
    
    void Update()  
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(20);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for one second

            // Increase currentHealth by regenAmountPerSecond, but not exceeding maxHealth
            currentHealth = Mathf.Min(currentHealth + regenAmountPerSecond, maxHealth);

            // Update the health bar
            healthBar.SetHealth(currentHealth);
        }
    }
}
