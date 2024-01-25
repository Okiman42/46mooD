using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerMovement : MonoBehaviour
{
    public float speed = 5f;            // Player movement speed
    public float rotationSpeed = 120f;  // Player rotation speed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Lock cursor to center of the screen to capture input
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get player input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Move the player
        MovePlayer(direction);

        // Rotate the player
        RotatePlayer();
    }

    void MovePlayer(Vector3 direction)
    {
        // Calculate movement vector
        Vector3 moveVector = direction * speed * Time.deltaTime;

        // Move the player using Rigidbody
        rb.MovePosition(rb.position + transform.TransformDirection(moveVector));
    }

    void RotatePlayer()
    {
        // Calculate rotation amount based on player input
        float rotationAmount = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // Rotate the player using Rigidbody
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationAmount);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
