using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 180f;
    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.2f;

    private float timeSinceLastShake;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Rotate the player based on input
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if the player is moving and trigger camera shake
        if (movement.magnitude > 0.1f)
        {
            if (Time.time - timeSinceLastShake > shakeDuration)
            {
                StartCoroutine(ShakeCamera());
                timeSinceLastShake = Time.time;
            }
        }
    }

    IEnumerator ShakeCamera()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeAmount;
            float yOffset = Random.Range(-1f, 1f) * shakeAmount;
            float zOffset = Random.Range(-1f, 1f) * shakeAmount;

            Camera.main.transform.position = new Vector3(
                initialPosition.x + xOffset,
                initialPosition.y + yOffset,
                initialPosition.z + zOffset
            );

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.position = initialPosition;
    }
}
