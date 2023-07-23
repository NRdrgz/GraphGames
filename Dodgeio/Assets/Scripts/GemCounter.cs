using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    public GameObject gemtextMain;
    public GameObject gemtextGame;
    public int currentGemsMain = 0;
    public int currentGemsGame = 0;
    public static GemCounter instance;

    private void Awake()
    {
        instance = this;
        currentGemsMain = PlayerPrefs.GetInt("CollectedGems", 0);
        gemtextMain.GetComponent<TextMeshProUGUI>().text = currentGemsMain.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGemsGame = 0;
        gemtextGame.GetComponent<TextMeshProUGUI>().text = currentGemsGame.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseGems(int v, string increaseType)
    {
        if (increaseType == "Main")
        {
            currentGemsMain = currentGemsMain + v;
            PlayerPrefs.SetInt("CollectedGems", currentGemsMain);
            iTween.PunchScale(gemtextMain, new Vector3(2, 2, 2), 1.0f);
            gemtextMain.GetComponent<TextMeshProUGUI>().text = currentGemsMain.ToString();

        }

        else if (increaseType == "Game")
        {
            currentGemsGame = currentGemsGame + v;
            iTween.PunchScale(gemtextGame, new Vector3(2, 2, 2), 1.0f);
            gemtextGame.GetComponent<TextMeshProUGUI>().text = currentGemsGame.ToString();

        }
    }
}
