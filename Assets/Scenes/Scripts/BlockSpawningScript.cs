using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawningScript : MonoBehaviour
{
    public GameObject[] shapes; // Assign your shape prefabs here in the inspector
    public float dropInterval = 1.0f;

    private void Start()
    {
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

        // Spawn the shape at a random x position, rounded to the nearest unit for grid alignment
        float x = Mathf.Round(Random.Range(-5.0f, 5.0f));

        // Instantiate the shape
        GameObject shape = Instantiate(shapes[randomIndex]);

        // Special condition for 'OShape' or 'IShape'
        if (shape.name.Contains("OShape") || shape.name.Contains("IShape"))
        {
            x -= 0.5f;
        }

        // If the shape has a pivot point defined
        Transform pivot = shape.transform.Find("PivotPoint");  // Replace "PivotPoint" with the name of your pivot GameObject

        if (pivot != null)
        {
            Vector3 offset = pivot.localPosition;

            // Adjust the spawn position based on the local position of the pivot point
            shape.transform.position = new Vector3(x - offset.x, 10 - offset.y, 0);
        }
        else
        {
            shape.transform.position = new Vector2(x, 10);
        }

        shape.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }


}
