using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth = 100f;   // Maximum health of the enemy
    private float currentHealth;     // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        // Reduce health by the specified amount
        currentHealth -= amount;

        // Check if the enemy is still alive
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle death logic here
        // For example, play death animations, spawn particles, etc.
        Debug.Log($"{gameObject.name} has been defeated!");
        Destroy(gameObject);
    }
}
