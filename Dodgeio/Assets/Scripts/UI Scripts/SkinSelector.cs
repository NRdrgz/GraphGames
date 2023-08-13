using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public GameObject shop;

    public void SelectSkin(int skinId)
    {
        PlayerPrefs.SetInt("SelectedSkin", skinId); // Change the skin

        //Update the character to update the skin
        GameObject.Find("CharacterSpawner").GetComponent<SpawnCharacter>().ChangeSkin();

        shop.SetActive(false); //remove the shop

    }
}
