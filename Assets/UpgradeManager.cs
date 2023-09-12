using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [HideInInspector]
    // Speed upgrade variables
    public int speedLevel = 0;
    [HideInInspector]
    public int speedPrice = 10;
    [HideInInspector]
    // Building upgrade variables
    public int buildingLevel = 0;
    [HideInInspector]
    public int buildingPrice = 20;
    [HideInInspector]
    // Rate upgrade variables
    public int rateLevel = 0;
    [HideInInspector]
    public int ratePrice = 30;
    [SerializeField]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI buildingText;
    public TextMeshProUGUI rateText;

    void Update()
    {
        // Update the TextMeshPro objects with the current levels and prices
        UpdateText(speedText, "Speed", speedLevel, speedPrice);
        UpdateText(buildingText, "Building", buildingLevel, buildingPrice);
        UpdateText(rateText, "Rate", rateLevel, ratePrice);

        // Check for user input to upgrade
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Upgrade(ref speedLevel, ref speedPrice);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Upgrade(ref buildingLevel, ref buildingPrice);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Upgrade(ref rateLevel, ref ratePrice);
        }
    }

    void UpdateText(TextMeshProUGUI textObject, string upgradeName, int level, int price)
    {
        string priceColor = (UIManager.Instance._money >= price) ? "green" : "red";
        textObject.text = $"{upgradeName} ({level}) $<color={priceColor}>{price}</color>";
    }

    void Upgrade(ref int level, ref int price)
    {
        if (UIManager.Instance._money >= price)
        {
            UIManager.Instance._money -= price;
            level++;
            price += level * price;
        }
    }
}
