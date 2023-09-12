using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject popupUIPrefab;
    public TMP_Text moneyText;
    public List<GameObject> buildingPlaceHolders;

    private int _money;
    
    private void Awake()
    {
        Instance = this;
    }

    public void PlaceBuilding(int numGlass, int numWall, int numRoof, Transform building)
    {
        BuildingPlaced targetPlaceholder = null;
        foreach (GameObject ph in buildingPlaceHolders)
        {
            var placeholder = ph.GetComponent<BuildingPlaced>();
            if (!placeholder.empty) 
                continue;

            targetPlaceholder = placeholder;
        }

        if (targetPlaceholder == null)
            return;
        targetPlaceholder.Initialize(numGlass, numWall, numRoof, building);
    }

    public void SpawnMoneyPopup(Vector3 pos, int money)
    {
        var ui = Instantiate(popupUIPrefab, pos, Quaternion.identity);
        ui.GetComponent<PopupUI>().Initialize($"+${money}");
        _money += money;
        moneyText.text = $"Money: ${_money}";
    }
}
