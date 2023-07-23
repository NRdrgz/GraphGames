using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{

    public GameObject[] characterPrefabs;
    private GameObject characterPrefab;
    private Vector3 characterPosition;
    private int selectedSkin;

    private void Awake()
    {
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0); //Retrieve the saved skin, 0 by default
        characterPrefab = characterPrefabs[selectedSkin];
    }


    void Start()
    {

        CreateCharacter();


    }

    void CreateCharacter()
    {
        //Select a random tile to spawn ont
        int row = Random.Range(0, GridManager.Instance.numColumns);
        int col = Random.Range(0, GridManager.Instance.numRows);
        characterPosition = new Vector3(row, -0.038f, col - 0.5f);
        GameObject character = Instantiate(characterPrefab, characterPosition, Quaternion.identity);
        character.AddComponent<MovementCharacter>();
        //scale character same as spawner
        character.transform.localScale = transform.localScale;
        //rename game object
        character.name = "MainPlayer";

    }

    public void ChangeSkin() //To change the skin when clicking in the shop
    {
        Destroy(GameObject.Find("MainPlayer"));
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0); //Retrieve the saved skin, 0 by default
        characterPrefab = characterPrefabs[selectedSkin];
        CreateCharacter();
    }


  
    // Update is called once per frame
    void Update()
    {
        
    }
}
