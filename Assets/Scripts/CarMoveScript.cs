using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveScript : MonoBehaviour
{
    public float speed = 8.0f; // Speed at which the GameObject will move

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input (a/left arrow is -1, d/right arrow is 1)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the new position
        Vector3 newPosition = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        // Update the GameObject's position
        transform.position = newPosition;
    }
}
