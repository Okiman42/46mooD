using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;            // Reference to the player's Transform
    public GameObject bulletPrefab;     // Prefab of the bullet to be fired
    public Transform firePoint;         // Point where the bullets will be spawned
    public float fireRate = 2f;         // Shots per second
    public int burstSize = 3;           // Number of shots in a burst
    public float burstCooldown = 1f;    // Cooldown between bursts
    public float bulletSpeed = 10f;     // Speed of the fired bullets
    public float damage = 10f;          // Damage dealt to the player
    public LayerMask obstacleMask;      // Layer mask for obstacles that can block line of sight

    private float nextTimeToShoot = 0f;
    private bool isShooting = false;

    void Update()
    {
        // Rotate the enemy to face the player
        RotateTowardsPlayer();

        // Check if it's time to shoot
        if (Time.time >= nextTimeToShoot && CanSeePlayer())
        {
            if (!isShooting)
            {
                StartCoroutine(ShootBurst());
            }

            // Set the next time to shoot based on the fire rate
            nextTimeToShoot = Time.time + 1f / fireRate;
        }
    }

    void RotateTowardsPlayer()
    {
        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Ensure the enemy remains upright when looking at the player
        directionToPlayer.y = 0;

        // Correct for the initial 90-degree rotation on the X-axis
        Quaternion rotationCorrection = Quaternion.Euler(90f, 0f, 0f);
        directionToPlayer = rotationCorrection * directionToPlayer;

        // Rotate the enemy to face the player over time
        float rotationSpeed = 5f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * rotationSpeed);
    }

    bool CanSeePlayer()
    {
        // Check if there is a clear line of sight to the player using a raycast
        Vector3 directionToPlayer = player.position - transform.position;

        // Correct for the initial 90-degree rotation on the X-axis
       /* Quaternion rotationCorrection = Quaternion.Euler(90f, 180f, 0f);
        directionToPlayer = rotationCorrection * directionToPlayer;

        Quaternion rotationCorrectionZ = Quaternion.Euler(0f, 0f, 180f);
        directionToPlayer = rotationCorrectionZ * directionToPlayer;
       */
        float distanceToPlayer = directionToPlayer.magnitude;

        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
        {
            // The ray hit an obstacle, so the player is not visible
            return false;
        }

        // The player is visible
        return true;
    }

    IEnumerator ShootBurst()
    {
        isShooting = true;

        // Perform a 3-round burst
        for (int i = 0; i < burstSize; i++)
        {
            Shoot();
            yield return new WaitForSeconds(1f / fireRate);
        }

        // Cooldown between bursts
        yield return new WaitForSeconds(burstCooldown);

        isShooting = false;
    }

    void Shoot()
    {
        // Create a bullet and set its properties
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Set the initial velocity
        bulletRb.velocity = bullet.transform.forward * bulletSpeed;

        // Set the bullet damage directly
        BulletHitbox bulletHitbox = bullet.GetComponent<BulletHitbox>();
        if (bulletHitbox != null)
        {
            bulletHitbox.SetDamage(damage);
        }

        // Destroy the bullet after a certain time (adjust as needed)
        Destroy(bullet, 2f);
    }
}
