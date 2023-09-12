using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject popupUIPrefab;
    public TMP_Text moneyText;

    private int _money;
    
    private void Awake()
    {
        Instance = this;
    }

    public void SpawnMoneyPopup(Vector3 pos, int money)
    {
        var ui = Instantiate(popupUIPrefab, pos, Quaternion.identity);
        ui.GetComponent<PopupUI>().Instantiate($"+${money}");
        _money += money;
        moneyText.text = $"Money: ${_money}";
    }
}
