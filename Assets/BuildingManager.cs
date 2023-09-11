using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private bool isCoroutineRunning = false;
    private float currentStackHeight = 0f; // To keep track of the height of the stack

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Truck" && !isCoroutineRunning)
        {
            StartCoroutine(RemoveFromStackEverySecond(collider.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Truck")
        {
            isCoroutineRunning = false;
        }
    }

    IEnumerator RemoveFromStackEverySecond(GameObject truck)
    {
        isCoroutineRunning = true;

        while (isCoroutineRunning)
        {
            truck.GetComponent<TruckManager>().removeFromStack();
            yield return new WaitForSeconds(1f);
        }
    }

    public void PlaceBuildingPart(GameObject buildingPart)
    {
        // Make the buildingPart a child of the building
        buildingPart.transform.SetParent(transform);
        Rigidbody2D rb = buildingPart.GetComponent<Rigidbody2D>();
        Collider2D col = buildingPart.GetComponent<Collider2D>();
        if (rb != null) rb.isKinematic = true;
        if (col != null) col.enabled = false;
        // Calculate the position to move the popped building part to
        float buildingBottomY = transform.position.y - GetComponent<Collider2D>().bounds.extents.y;
        float buildingPartHalfHeight = buildingPart.GetComponent<Collider2D>().bounds.extents.y + 0.5f;
        Vector3 newPosition = new Vector3(transform.position.x, buildingBottomY + currentStackHeight + buildingPartHalfHeight, buildingPart.transform.position.z);

        // Move the popped building part to the new position
        buildingPart.transform.position = newPosition;

        // Update the current stack height
        currentStackHeight += 0.5f + buildingPartHalfHeight;
    }
}
