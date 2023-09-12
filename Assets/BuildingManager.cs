using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private bool isCoroutineRunning = false;
    private float currentStackHeight = 0f; // To keep track of the height of the stack
    [SerializeField]
    public NewBuildingBox newBuildingBox;
    private UIManager moneyManager;

    private int numGlass;
    private int numRoof;
    private int numWall;

    // needs a wall before it can be placed.
    private bool isBuildingValid = false;

    private void Start()
    {
        GameObject.FindWithTag("Truck").GetComponent<TruckManager>().buildingManager = this.gameObject.GetComponent<BuildingManager>();
        GameObject craneObject = GameObject.Find("Crane");
        moneyManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        print(moneyManager.name);

        if (craneObject != null)
        {
            Debug.Log("Found GameObject: " + craneObject.name);
            newBuildingBox = craneObject.GetComponent<NewBuildingBox>();
        }
        else
        {
            Debug.Log("GameObject not found");
        }
    }

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
        this.tag = "Untagged";
        // Make the buildingPart a child of the building
        buildingPart.transform.SetParent(transform);
        Rigidbody2D rb = buildingPart.GetComponent<Rigidbody2D>();
        Collider2D col = buildingPart.GetComponent<Collider2D>();

        if (rb != null) rb.isKinematic = true;
        //if (col != null) col.enabled = false;
        
        // Calculate the position to move the popped building part to
        float buildingBottomY = transform.position.y - GetComponent<Collider2D>().bounds.extents.y;
        float buildingPartHalfHeight = buildingPart.GetComponent<Collider2D>().bounds.extents.y + 0.1f;
        Vector3 newPosition = new Vector3(transform.position.x, buildingBottomY + currentStackHeight + buildingPartHalfHeight, 2);

        // Move the popped building part to the new position
        buildingPart.transform.position = newPosition;

        // Update the current stack height
        currentStackHeight += 0.5f + buildingPartHalfHeight;

        if(buildingPart.name.Contains("Wall"))
        {
            numWall += 1;
            isBuildingValid = true;
        } else if (buildingPart.name.Contains("Glass"))
        {
            if (!isBuildingValid)
            {
                DeathManager.Instance.restartGame("No Wall for Foundation");
            }
            numGlass += 1;
        } else if (buildingPart.name.Contains("Roof"))
        {
            if (!isBuildingValid)
            {
                DeathManager.Instance.restartGame("No Wall for Foundation");
            }
            numRoof += 1;
            finishBuilding();
        }
    }

    public void finishBuilding()
    {
        Transform parent = gameObject.transform.parent;



        // disable red bar
        Destroy(parent.GetChild(1).gameObject);
        // lower the opacity of the building box
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        // remove the active tag
        this.gameObject.tag = "Untagged";
        parent.gameObject.tag = "Untagged";
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        UIManager.Instance.PlaceBuilding(numGlass, numWall, numRoof, this.transform);
        DeathManager.Instance.numOfBuildings += 1;

        // Spawn in new building box
        Vector3 spawnPosition = transform.position + new Vector3(6, 0, 0);
        newBuildingBox.spawnBuilding(spawnPosition);
    }
}
