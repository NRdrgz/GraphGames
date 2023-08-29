using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedIncrementalController : MonoBehaviour
{
    public int speedLevel;
    public int initialSpeedPrice = 500;
    public GameObject speedPriceText;
    public GameObject speedLevelText;
    private int currentGemsMain;
    private int speedPrice;
    

    // Start is called before the first frame update
    void Start()
    {
        speedLevel = PlayerPrefs.GetInt("SpeedLevel", 1); //Get the user speed level
        speedPrice = initialSpeedPrice * (speedLevel); //Increase by 100% for each level
        speedPriceText.GetComponent<TextMeshProUGUI>().text = speedPrice.ToString(); //Update text of UI
        speedLevelText.GetComponent<TextMeshProUGUI>().text = "Lvl " + speedLevel.ToString(); //Update text of UI
    }


    // The user can pay with gems if it has it, or watch an RV otherwise
    public void ClickSpeedIncremental()
    {
        currentGemsMain = PlayerPrefs.GetInt("CollectedGems", 0);
        if (currentGemsMain >= speedPrice)
        {
            speedLevel++;
            PlayerPrefs.SetInt("SpeedLevel", speedLevel); //increase speed level
            speedLevelText.GetComponent<TextMeshProUGUI>().text = "Lvl " + speedLevel.ToString(); //Update text of UI

            GemCounter.instance.IncreaseGems(-speedPrice, "Main"); //Decrease number of gems
            speedPrice = initialSpeedPrice * (speedLevel); //Increase by 100% for each level

            speedPriceText.GetComponent<TextMeshProUGUI>().text = speedPrice.ToString(); //Update text of UI

        }
    }
}
