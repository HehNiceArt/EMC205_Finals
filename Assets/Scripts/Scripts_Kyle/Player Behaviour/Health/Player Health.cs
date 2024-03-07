using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth;

    public HealthBar healthBar;
   

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

  
    // Just add a code how the player can inflict damage
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthBar.SetHealth(currentHealth);
    }
}
