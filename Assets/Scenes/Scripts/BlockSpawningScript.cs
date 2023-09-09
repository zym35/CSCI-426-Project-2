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

        // Spawn the shape at a random x position
        float x = Random.Range(-5.0f, 5.0f);
        GameObject shape = Instantiate(shapes[randomIndex], new Vector2(x, 10), Quaternion.identity);

        shape.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }


}
