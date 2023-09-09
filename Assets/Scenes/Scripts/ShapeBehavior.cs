using System.Collections;
using UnityEngine;

public class ShapeBehaviour : MonoBehaviour
{
    public Transform pivotPoint;  // Assign the Pivot GameObject here in the Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Locked")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            LockBlocks();
            SnapToGrid();
            SnapRotation();
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
}
