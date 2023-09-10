using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressChecker : MonoBehaviour
{
    private float totalMass = 0.0f; // Declare this variable at the top of your script
    public Slider massSlider;
    private float maxMass = 100f;  // Set the maximum mass here
    private float currentMass = 0f;

    // Call this function in OnCollisionEnter2D to add mass
    public void AddMassFromParent(GameObject shape)
    {
        totalMass += shape.GetComponent<Rigidbody2D>().mass;

        // Clamp the mass value so that it stays within the limits
        currentMass = Mathf.Clamp(totalMass, 0f, maxMass);

        // Option 1: Update a UI Slider
        massSlider.value = totalMass / maxMass;
    }
}
