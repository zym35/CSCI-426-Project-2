using System.Collections;
using UnityEngine;

public class ShapeBehaviour : MonoBehaviour
{
    public Transform pivotPoint;  // Assign the Pivot GameObject here in the Inspector
    private string shaderColorProperty = "_Color"; // The name of the color property in your shader
    private float pulseDuration = 0.1f;
    private float totalMass = 0.0f; // Declare this variable at the top of your script
    public ProgressChecker pc;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Locked" && this.tag != "Locked")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            LockBlocks();
            pc = GameObject.Find("ProgressManager").GetComponent<ProgressChecker>();
            pc.AddMassFromParent(this.gameObject);
            SnapToGrid();
            SnapRotation();
            StartCoroutine(Pulse());
        }
    }

    private void LockBlocks()
    {
        this.tag = "Locked";
    }

    private void SnapToGrid()
    {
        // Rounding to the nearest integer for Unity unit-based grid
        float x = Mathf.Round(transform.position.x);
        float y = Mathf.Round(transform.position.y);
        transform.position = new Vector2(x, y);
    }


    private void SnapRotation()
    {
        Vector3 rotation = pivotPoint.eulerAngles;
        float z = Mathf.Round(rotation.z / 90f) * 90f;
        pivotPoint.eulerAngles = new Vector3(0, 0, z);  // Set the child rotation

        // Also set the parent rotation
        this.transform.eulerAngles = new Vector3(0, 0, z);
    }

    IEnumerator Pulse()
    {
        // Disable colliders in all children
        //Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        //foreach (Collider2D col in colliders)
        //{
        //    col.enabled = false;
        //}

        // Store original scale
        Vector3 originalScale = transform.localScale;
        Vector3 pulseScaleUp = originalScale * 1.2f;

        // Store original colors and materials for each child
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        Color[] originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.GetColor(shaderColorProperty);
        }

        Color pulseColorWhite = Color.white;

        // Scale up and fade to white
        for (float t = 0; t <= 1; t += Time.deltaTime / pulseDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, pulseScaleUp, t);
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetColor(shaderColorProperty, Color.Lerp(originalColors[i], pulseColorWhite, t));
            }
            yield return null;
        }

        // Scale down and fade back to original color
        for (float t = 0; t <= 1; t += Time.deltaTime / pulseDuration)
        {
            transform.localScale = Vector3.Lerp(pulseScaleUp, originalScale, t);
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetColor(shaderColorProperty, Color.Lerp(pulseColorWhite, originalColors[i], t));
            }
            yield return null;
        }

        //// Re-enable colliders in all children
        //foreach (Collider2D col in colliders)
        //{
        //    col.enabled = true;
        //}
    }
}
