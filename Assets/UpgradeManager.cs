using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [HideInInspector]
    // Speed upgrade variables
    private int speedLevel = 1;
    [HideInInspector]
    public int speedPrice = 10;
    [HideInInspector]
    // Building upgrade variables
    private int buildingLevel = 1;
    [HideInInspector]
    public int buildingPrice = 20;
    [HideInInspector]
    // Rate upgrade variables
    private int rateLevel = 1;
    [HideInInspector]
    public int ratePrice = 30;
    private int maxLevel = 15;
    [SerializeField]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI buildingText;
    public TextMeshProUGUI rateText;

    public CarMoveScript truckScript;
    private BuildingPlaced[] buildings;

    private void Start()
    {
        // Find all active GameObjects with the BuildingPlaced script attached
        buildings = FindObjectsOfType<BuildingPlaced>();
    }

    void Update()
    {
        // Update the TextMeshPro objects with the current levels and prices
        UpdateText(speedText, "[1] Speed", speedLevel, speedPrice);
        UpdateText(buildingText, "[2] Building", buildingLevel, buildingPrice);
        UpdateText(rateText, "[3] Rate", rateLevel, ratePrice);

        // Check for user input to upgrade
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Upgrade(1, ref speedLevel, ref speedPrice);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Upgrade(2, ref buildingLevel, ref buildingPrice);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Upgrade(3, ref rateLevel, ref ratePrice);
        }
    }

    void UpdateText(TextMeshProUGUI textObject, string upgradeName, int level, int price)
    {
        if (level>= maxLevel){
            string priceColor = "black";
            textObject.text = $"{upgradeName} ({level}) $<color={priceColor}>MAXED</color>";
        }
        else
        {
            string priceColor = (UIManager.Instance._money >= price) ? "00A10A" : "A10007";
            textObject.text = $"{upgradeName} ({level}) $<color=#{priceColor}>{price}</color>";
        }
    }

    void Upgrade(int upgradeType, ref int level, ref int price)
    {
        if (UIManager.Instance._money >= price && level < 15)
        {
            UIManager.Instance._money -= price;
            level++;
            price += level * level * price;

            switch (upgradeType)
            {
                case 1:
                    truckScript.initialSpeed += 1;
                    if (level % 5 == 0)
                    {
                        truckScript.acceleration += 1;
                    }
                    break;
                case 2:
                    // Iterate through the array and do something with each building
                    foreach (BuildingPlaced building in buildings)
                    {
                        building.moneyRate /= (level - 1);
                        building.moneyRate *= level;
                    }
                    break;
                case 3:
                    foreach (BuildingPlaced building in buildings)
                    {
                        building.generateMoneyInterval *= (level - 1);
                        building.generateMoneyInterval /= level;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
