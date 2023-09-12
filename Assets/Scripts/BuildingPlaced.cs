using System.Collections;
using UnityEngine;

public class BuildingPlaced : MonoBehaviour
{
    public Transform popupUISpawn;
    public float generateMoneyInterval;
    public int moneyRate;
    public bool empty = true;

    public void Initialize(int numGlass, int numWall, int numRoof, Transform building)
    {
        Instantiate(this);
        moneyRate = (numWall + numGlass * 2) + numRoof;
        empty = false;


        // Find the child with a name containing "Roof"
        Transform roofTransform = null;
        foreach (Transform child in building)
        {
            if (child.name.Contains("Roof"))
            {
                roofTransform = child;
                break;
            }
        }

        if (roofTransform != null)
        {
            // Calculate the top position of the "Roof"
            float roofHeight = roofTransform.localScale.y;
            Vector3 topOfRoof = roofTransform.position + new Vector3(0, roofHeight / 2, 0);

            // Set the position of this GameObject to the top of the "Roof"
            this.transform.position = topOfRoof;
        }
        else
        {
            Debug.LogWarning("No child containing 'Roof' in its name found.");
        }

        //building.SetParent(transform);
        //building.localPosition = Vector3.zero;
        //building.localScale *= 0.5f;
        StartCoroutine(GenerateMoney());
    }

    private IEnumerator GenerateMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(generateMoneyInterval);
            UIManager.Instance.SpawnMoneyPopup(popupUISpawn.position, moneyRate);
        }
    }
}
