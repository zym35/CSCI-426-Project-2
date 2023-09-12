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
        moneyRate = (numWall + numRoof * 2) * numGlass;
        empty = false;
        building.SetParent(transform);
        building.localPosition = Vector3.zero;
        building.localScale *= 0.5f;
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
