using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    [SerializeField] float currentHealth;

    //[SerializeField] private float _lives;

    //[SerializeField] private LayerMask selectedTag;

    void Start()
    {
        currentHealth = maxHealth;
        //_lives = currentHealth;
       // Debug.Log(_lives);
        Debug.Log(currentHealth + " currenthealth start");
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("collided");
        // Check if the player collides with an object tagged as "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Assume the bullet deals a fixed amount of damage (you can customize this)
            float bulletDamage = 1f;
            TakeDamage(bulletDamage);

            // Destroy the bullet on impact
            Destroy(other.gameObject);

            
        }

    }

    void TakeDamage(float damage)
    {
        // Reduce the player's health
        currentHealth -= damage;
        Debug.Log(damage);
       // Debug.Log(currentHealth);
        // Check if the player is dead
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // You can implement death behavior here, such as respawning or game over
        Debug.Log("Player has died!");
        // For now, let's just deactivate the player GameObject
        gameObject.SetActive(false);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

   
}
