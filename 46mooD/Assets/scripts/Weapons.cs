using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public LineRenderer lineRenderer;       // Reference to the LineRenderer component

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
            Quaternion spreadRotation = Quaternion.Euler(
                Random.Range(-spreadFactor, spreadFactor),
                Random.Range(-spreadFactor, spreadFactor),
                0f
            );

            Ray ray = new Ray(shootPoint.position, spreadRotation * shootPoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range, targetLayer))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Damageable target = hit.transform.GetComponent<Damageable>();
                    if (target != null)
                    {
                        target.TakeDamage(pelletDamage);
                    }
                }

                // Set LineRenderer positions
                lineRenderer.SetPosition(0, shootPoint.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                // If the ray doesn't hit anything, draw the line till the maximum range
                lineRenderer.SetPosition(0, shootPoint.position);
                lineRenderer.SetPosition(1, shootPoint.position + shootPoint.forward * range);
            }
        }
    }

    void ShootArrow()
    {
        Quaternion spreadRotation = Quaternion.Euler(
            Random.Range(-spreadFactor, spreadFactor),
            Random.Range(-spreadFactor, spreadFactor),
            0f
        );

        Ray ray = new Ray(shootPoint.position, spreadRotation * shootPoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, targetLayer))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Damageable target = hit.transform.GetComponent<Damageable>();
                if (target != null)
                {
                    target.TakeDamage(arrowDamage);
                }
            }

            // Set LineRenderer positions
            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If the ray doesn't hit anything, draw the line till the maximum range
            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, shootPoint.position + shootPoint.forward * range);
        }
    }
}

