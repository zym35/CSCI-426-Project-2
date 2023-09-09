using UnityEngine;

public class ShapeBehaviour : MonoBehaviour
{
    public float gridSize = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        SnapToGrid();
        SnapRotation();
    }

    private void SnapToGrid()
    {
        // Calculate the nearest grid positions
        float x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float y = Mathf.Round(transform.position.y / gridSize) * gridSize;

        // Snap the object to the new position
        transform.position = new Vector2(x, y);
    }

    private void SnapRotation()
    {
        float z = Mathf.Round(transform.eulerAngles.z / 90f) * 90f;
        transform.eulerAngles = new Vector3(0, 0, z);
    }
}
