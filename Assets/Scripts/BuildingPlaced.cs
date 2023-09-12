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
    }

    private void Start()
    {
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
