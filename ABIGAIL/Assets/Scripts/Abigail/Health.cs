using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public HealthBar healthBar;
    public Vector2 respawnPoint; // The position where the player will respawn
    public float fallThreshold = -10f; // The y-coordinate that triggers a respawn if the player falls below it

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        // Check for falling below the threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        
        // Example: If health drops to or below 0, destroy the object
        if (currentHealth <= 0)
        {
            Debug.Log("GAME OVER");
            Respawn();

        }
    }
    private void Respawn()
    {
        Debug.Log("Respawning player.");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        transform.position = respawnPoint; // Move the player to the respawn point

        // If you have a Rigidbody2D and need to reset velocity, add this:
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}

