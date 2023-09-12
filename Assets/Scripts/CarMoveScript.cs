using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveScript : MonoBehaviour
{
    private float initialSpeed = 5f; // Initial speed
    private float maxSpeed = 20f; // Maximum speed
    private float acceleration = 2f; // Acceleration rate
    private float currentSpeed; // Current speed
    private Camera cam;
    private float halfCamWidth;

    void Start()
    {
        cam = Camera.main;
        halfCamWidth = cam.orthographicSize * cam.aspect;
        currentSpeed = initialSpeed;
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply acceleration if moving
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // Reset speed when not moving
            currentSpeed = initialSpeed;
        }

        // Clamp the speed
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed, maxSpeed);

        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
        transform.position += movement * Time.deltaTime * currentSpeed;

        // Get camera position and limits
        Vector3 camPosition = cam.transform.position;
        float leftLimit = camPosition.x - halfCamWidth - 3;
        float rightLimit = camPosition.x + halfCamWidth;

        // Clamp the truck's position to the camera's bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftLimit, rightLimit);
        transform.position = clampedPosition;
    }
}
