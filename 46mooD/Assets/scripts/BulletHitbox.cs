using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitbox : MonoBehaviour
{
    private float damage;

    public void SetDamage(float value)
    {
        damage = value;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the bullet collides with a player (or other damageable objects)
        if (other.CompareTag("Player"))
        {
            // Deal damage to the player (or other damageable objects)
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            // Destroy the bullet on impact
            Destroy(gameObject);
        }
    }
}

