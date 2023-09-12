using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuildingBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject prefabToSpawn;

    public void spawnBuilding(Vector3 spawnPosition)
    {
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
