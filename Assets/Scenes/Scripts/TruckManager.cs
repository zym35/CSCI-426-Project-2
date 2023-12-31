using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public Stack<GameObject> buildingParts;
    [SerializeField]
    public BuildingManager buildingManager;
    // Start is called before the first frame update
    void Start()
    {
        buildingParts = new Stack<GameObject>();
        buildingManager = GameObject.FindWithTag("ActiveBuildingBox").GetComponent<BuildingManager>();
    }

    public void addToStack(GameObject newObject)
    {
        if(buildingParts.Count > 0)
        {
            GameObject topObject = buildingParts.Peek();
            if (topObject.name.Contains("Roof")){
                DeathManager.Instance.restartGame("Can't Stack on Top of Roof");
            }
        }
        buildingParts.Push(newObject);
    }

    public void removeFromStack()
    {
        if(buildingParts.Count > 0)
        {
            GameObject poppedBuilding = buildingParts.Pop();
            buildingManager.PlaceBuildingPart(poppedBuilding);
        }
        
    }
}
