using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float pelletDamage = 10f;              
    public float arrowDamage = 40;
    public float fireRate = 1f;             
    public float range = 10f;               
    public int pelletsPerShot = 10;         
    public LayerMask targetLayer;           
    public Transform shootPoint;            
    public float spreadFactor = 5f;         

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            ShootShotgun();
        }

        if (Input.GetButtonDown("Fire2") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            ShootArrow();
        }
    }

    void ShootShotgun()
    {
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

            
            if (Physics.Raycast(ray, out hit, range, targetLayer))
            {
                Debug.Log("hit");
                
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("damage");
                    // Deal damage to the hit object
                    Damageable target = hit.transform.GetComponent<Damageable>();
                    if (target != null)
                    {
                        target.TakeDamage(pelletDamage);
                    }
                }
            }

            // Draw debug ray in the Scene view (optional)
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);
        }
    }

    void ShootArrow()
    {
        Quaternion spreadRotation = Quaternion.Euler(
            Random.Range(-spreadFactor, spreadFactor),
            Random.Range(-spreadFactor, spreadFactor),
            0f
        );

        // Create a ray from the shoot point with the spread rotation
        Ray ray = new Ray(shootPoint.position, spreadRotation * shootPoint.forward);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, range, targetLayer))
        {
            Debug.Log("hit");
            
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("damage");
                // Deal damage to the hit object
                Damageable target = hit.transform.GetComponent<Damageable>();
                if (target != null)
                {
                    target.TakeDamage(arrowDamage);
                }
            }
        }

        // Draw debug ray in the Scene view (optional)
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);
    }
}
