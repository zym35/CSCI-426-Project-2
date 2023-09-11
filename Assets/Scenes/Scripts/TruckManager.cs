using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public Stack<GameObject> buildingParts;
    // Start is called before the first frame update
    void Start()
    {
        buildingParts = new Stack<GameObject>();
    }

    public void addToStack(GameObject newObject)
    {
        buildingParts.Push(newObject);
    }

    public void removeFromStack()
    {
        if(buildingParts.Count > 0)
        {
            GameObject poppedBuilding = buildingParts.Pop();
            Destroy(poppedBuilding);
        }
        
    }
}
