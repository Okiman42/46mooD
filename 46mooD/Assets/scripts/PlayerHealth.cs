using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    private float currentHealth;   // Current health of the player

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        // Reduce health by the specified amount
        currentHealth -= amount;

        // Check if the player is still alive
        if (currentHealth <= 0)
        {
            Debug.Log("death");
            // Die();
        }
    }

    void Die()
    {
        // Handle death logic here
        // For example, play death animations, reset the level, etc.
        Debug.Log("Player has been defeated!");
        // For simplicity, we'll just disable the GameObject in this example
        gameObject.SetActive(false);
    }

}
