using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenControllerWin : MonoBehaviour
{
    private GameObject panel;
    private int gemIncrease;
    private int level;
    public GameObject levelCounter;
    // Start is called before the first frame update
    void Start()
    {
        panel = GridManager.Instance.panelControllerLose.gameObject;
        level = PlayerPrefs.GetInt("Level", 1); // Retrieve user level
        levelCounter.GetComponent<TextMeshProUGUI>().text = "Level " + level.ToString(); // Change the counter
        gameObject.SetActive(false); // Initially hide the panel
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true); // Show the panel

        level++; //increase the level
        PlayerPrefs.SetInt("Level", level);
        levelCounter.GetComponent<TextMeshProUGUI>().text = "Level " + level.ToString(); // Change the counter


        gemIncrease = GemCounter.instance.currentGemsGame; //Get the number of won gems
        GameObject.Find("MoneyScoreEnd").GetComponent<TextMeshProUGUI>().text = gemIncrease.ToString(); //show on text the increase
        GemCounter.instance.IncreaseGems(gemIncrease, "Main"); //increase the bank by the increase
    }
}
