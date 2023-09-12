using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuildingBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject prefabToSpawn;
    int numOfBuildings = 0;

    public void spawnBuilding(Vector3 spawnPosition)
    {
        // Instantiate the prefab at the given position
        GameObject buildingInstance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        numOfBuildings += 1;
        // Check if the prefab has any children
        if (buildingInstance.transform.childCount > 1)  // Changed to check for more than one child
        {
            // Get the first child of the prefab
            Transform firstChild = buildingInstance.transform.GetChild(0);

            // Scale the first child by 5 units in the Y-axis
            Vector3 newScale = new Vector3(6, 10 + 3 * numOfBuildings, 0);
            firstChild.localScale = newScale;

            // Adjust the position of the first child to keep its bottom edge at the original position
            Vector3 newPosition = spawnPosition + new Vector3(0, (3.0f * numOfBuildings / 2.0f) + 1, 0);
            firstChild.position = newPosition;

            // Get the second child of the prefab
            Transform secondChild = buildingInstance.transform.GetChild(1);

            // Position the second child at the top of the first child
            Vector3 secondChildPosition = newPosition + new Vector3(0, (3.0f * numOfBuildings / 2.0f) + 5, 0);
            secondChild.position = secondChildPosition;
            buildingInstance.transform.position = new Vector3(buildingInstance.transform.position.x, 0.0f, buildingInstance.transform.position.z);
        }
        else
        {
            Debug.LogWarning("The prefab does not have enough children to scale and position.");
        }
    }

}
