using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EndScreenControllerLose : MonoBehaviour
{
    private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = GridManager.Instance.panelControllerLose.gameObject;
        gameObject.SetActive(false); // Initially hide the panel
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true); // Show the panel
        //Show FS
        AdManager.instance.ShowInterstitial();
    }

    public void ReviveRV()
    {
        
        AdManager.instance.rvType = "Revive";
        AdManager.instance.ShowRewardedAd("Revive");
        
    }

    public void Respawn() //Called in the RV postback
    {
        GameObject.Find("CharacterSpawner").GetComponent<SpawnCharacter>().CreateCharacter();
        GridManager.Instance.playerIsAlive = true;
        gameObject.SetActive(false);
        Time.timeScale = 1;//Start the game again
    }


}
