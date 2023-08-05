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
    }

    public void ReviveRV()
    {
        //TODO WATHCH RV
        Respawn();
    }

    private void Respawn()
    {
        GameObject.Find("CharacterSpawner").GetComponent<SpawnCharacter>().CreateCharacter();
        GridManager.Instance.playerIsAlive = true;
        gameObject.SetActive(false);
    }


}
