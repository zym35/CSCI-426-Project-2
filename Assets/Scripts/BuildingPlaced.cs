using System.Collections;
using UnityEngine;

public class BuildingPlaced : MonoBehaviour
{
    public Transform popupUISpawn;
    public float generateMoneyInterval;
    public int moneyRate;

    public void Instantiate(int numGlass, int numWall, int numRoof)
    {
        moneyRate = (numWall + numRoof * 2) * numGlass;
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
