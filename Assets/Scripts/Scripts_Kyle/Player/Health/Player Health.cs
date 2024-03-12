using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int regenAmountPerSecond = 5; 
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);

       
        StartCoroutine(RegenerateHealth());
    }

    
    
    // Test if the system is working
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
            yield return new WaitForSeconds(1f); 

           
            currentHealth = Mathf.Min(currentHealth + regenAmountPerSecond, maxHealth);

            healthBar.SetHealth(currentHealth);
        }
    }
}
