using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawningScript : MonoBehaviour
{
    public GameObject[] shapes; // Assign your shape prefabs here in the inspector
    public Material yourMaterial; // Drag your material here in the inspector
    public float dropInterval = 1.0f;
    private Dictionary<string, Color> shapeColors;
    private float zOrder = 0f;  // Initialize to 0; increase this value for every new block


    private void Start()
    {
        shapeColors = new Dictionary<string, Color>
    {
        { "IShape", Color.white },
        { "JShape", Color.red },
        { "LShape", Color.red },
        { "OShape", Color.white },
        { "SShape", Color.red },
        { "TShape", Color.red },
        { "ZShape", Color.red }
    };
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {

            rend.material = yourMaterial;
        }
        StartCoroutine(DropShapes());
    }

    private IEnumerator DropShapes()
    {
        while (true)
        {
            SpawnShape();
            yield return new WaitForSeconds(dropInterval);
        }
    }

    private void SpawnShape()
    {
        int randomIndex = Random.Range(0, shapes.Length);

        // Round to the nearest unit to align with grid
        float x = Mathf.Round(Random.Range(-5.0f, 5.0f));

        // Create a new Vector3 for the position, adding in the zOrder for the z-axis
        Vector3 spawnPos = new Vector3(x, 10, zOrder);

        // Instantiate shape at random position
        GameObject shape = Instantiate(shapes[randomIndex], spawnPos, Quaternion.identity);

        // Increase the zOrder for next time
        zOrder += 0.1f;  // Increase this by however much is needed to separate the objects

        // Prepare to set color based on shape name
        string shapeName = shape.name.Split('(')[0];  // Remove the "(Clone)" part of the name

        // Prepare material to set color
        Material newMaterial = new Material(Shader.Find("Custom/ColoredInteriorBorder"));

        if (shapeColors.TryGetValue(shapeName, out Color shapeColor))
        {
            newMaterial.color = shapeColor;
            SetMaterialToAllChildren(shape, newMaterial);
        }

        // Rotate shape randomly by multiples of 90 degrees
        Transform pivotPoint = shape.transform.Find("Pivot");
        if (pivotPoint != null)
        {
            float randomRotation = 90f * Random.Range(0, 4);
            shape.transform.RotateAround(pivotPoint.position, Vector3.forward, randomRotation);
        }

        // Activate gravity
        shape.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }

    void SetMaterialToAllChildren(GameObject parentObject, Material newMaterial)
    {
        // Set the material to the parent object itself if it has a Renderer component
        Renderer parentRenderer = parentObject.GetComponent<Renderer>();
        if (parentRenderer != null)
        {
            parentRenderer.material = newMaterial;
        }

        // Now set the material to all child objects
        foreach (Transform child in parentObject.transform)
        {
            Renderer rend = child.gameObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = newMaterial;
            }
        }
    }


}
