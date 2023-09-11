using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBlockCollsionLogic : MonoBehaviour
{
    public float animationDuration = 1.0f; // Duration of the breaking animation
    public float maxRotationSpeed = 360f; // Maximum rotation speed in degrees per
    private bool isAttached = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Road")
        {
            StartCoroutine(BreakAnimation());
        }
        else if ((collision.gameObject.tag == "Truck" || collision.gameObject.tag == "Block") && !isAttached)
        {
            MoveToTopAndAttach(collision.gameObject);
            AddObjectToStack();
        }
    }

    private void MoveToTopAndAttach(GameObject target)
    {
        isAttached = true; // Mark as attached so it won't break or attach again

        // Calculate the top position of the target object
        float targetTopY = target.transform.position.y + target.GetComponent<Collider2D>().bounds.extents.y;
        float myHalfHeight = GetComponent<Collider2D>().bounds.extents.y;

        // Set the new position
        transform.position = new Vector3(target.transform.position.x, targetTopY + myHalfHeight, transform.position.z);

        // Make this object a child of the target object
        transform.SetParent(target.transform);
    }

    private void AddObjectToStack()
    {
        GameObject truck = GameObject.FindGameObjectWithTag("Truck");
        truck.GetComponent<TruckManager>().addToStack(this.gameObject);
    }

    IEnumerator BreakAnimation()
    {
        // Disable the collider so it won't trigger more collisions during the animation
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Get the initial color and scale
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color initialColor = spriteRenderer.color;
        Vector3 initialScale = transform.localScale;

        // Initialize animation variables
        float elapsedTime = 0;
        float progress;

        // Generate a random rotation speed
        float rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / animationDuration;

            // Ease out
            float easeOutProgress = Mathf.Sin(progress * Mathf.PI * 0.5f);

            // Change color to dark and fade out
            spriteRenderer.color = Color.Lerp(initialColor, new Color(0, 0, 0, 0), easeOutProgress);

            // Shrink to zero scale
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, easeOutProgress);

            // Apply random rotation
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        // Destroy the object
        Destroy(gameObject);
    }
}