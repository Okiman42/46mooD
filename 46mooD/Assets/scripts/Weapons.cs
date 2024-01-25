using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damage = 10f;              // Damage per shot
    public float fireRate = 1f;             // Shots per second
    public float range = 10f;               // Maximum shooting distance
    public int pelletsPerShot = 10;         // Number of pellets per shot
    public LayerMask targetLayer;           // Layer mask to filter targets
    public Transform shootPoint;            // Point where the shotgun shoots from
    public float spreadFactor = 5f;         // Spread factor for controlling shotgun spread

    private float nextTimeToFire = 0f;

    void Update()
    {
        // Check if it's time to shoot
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Play shotgun sound or particle effects here

        // Perform the shotgun spread
        for (int i = 0; i < pelletsPerShot; i++)
        {
            // Calculate spread by adding random rotation multiplied by the spread factor
            Quaternion spreadRotation = Quaternion.Euler(
                Random.Range(-spreadFactor, spreadFactor),
                Random.Range(-spreadFactor, spreadFactor),
                0f
            );

            // Create a ray from the shoot point with the spread rotation
            Ray ray = new Ray(shootPoint.position, spreadRotation * shootPoint.forward);
            RaycastHit hit;

            // Check if the ray hits something
            if (Physics.Raycast(ray, out hit, range, targetLayer))
            {
                Debug.Log("hit");
                // Check if the hit object has the specified tag
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("damage");
                    // Deal damage to the hit object
                    Damageable target = hit.transform.GetComponent<Damageable>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }
                }
            }

            // Draw debug ray in the Scene view (optional)
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);
        }
    }
}
