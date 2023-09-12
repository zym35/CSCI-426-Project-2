using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Required for scene management
using System.Collections;


public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;

    // Buildings
    public int numOfBuildings = 0;
    public int totalRoofs = 0;
    public int totalGlass = 0;
    public int totalWalls = 0;
    // Upgrades / Money
    public int netIncome = 0;
    public int speedUpgrade = 1;
    public int buildingUpgrade = 1;
    public int rateUpgrade = 1;

    public TextMeshProUGUI deathMessage;

    public TextMeshProUGUI roofText;
    public TextMeshProUGUI glassText;
    public TextMeshProUGUI wallText;
    public TextMeshProUGUI totalBuildingText;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI buildingText;
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI netIncomeText;

    private void Awake()
    {
        Instance = this;
    }

    public void restartGame(string deathString)
    {
        StartCoroutine(ExecuteAfterDelay(0.1f, deathString));
    }

    void Update()
    {
        // Check for Enter key press
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // Check for Enter key press
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            // Reload the current scene
            restartGame("Manually Died by Press Delete");
        }
    }

    IEnumerator ExecuteAfterDelay(float delay, string deathString)
    {
        yield return new WaitForSeconds(delay);


        // Destroy all GameObjects except the camera and DeathManager
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj != this.gameObject && obj.tag != "MainCamera")
            {
                Destroy(obj);
            }
            if (obj.tag == "MainCamera")
            {
                obj.GetComponent<CameraManager>().enabled = false;
            }
        }

        deathMessage.text = deathString;

        roofText.text = $"{totalRoofs}";
        glassText.text = $"{totalGlass}";
        wallText.text = $"{totalWalls}";
        totalBuildingText.text = $"{numOfBuildings}";

        speedText.text = $"Lvl. {speedUpgrade}";
        buildingText.text = $"Lvl. {buildingUpgrade}";
        rateText.text = $"Lvl. {rateUpgrade}";
        netIncomeText.text = $"${netIncome}";

        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}