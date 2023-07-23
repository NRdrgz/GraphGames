using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlockGemBehavior : MonoBehaviour
{
    private int skinPrice;
    public int initialPrice;
    private int nbrUnlockedSkins;

    public void Awake()
    {
        //TODO: Change variables if more skins are added

        nbrUnlockedSkins = PlayerPrefs.GetInt("NbrUnlockedSkins", 0); //Retrieve the number of unlocked skins
        skinPrice = initialPrice * (1 + nbrUnlockedSkins); //Increase the price by 100% for each unlocked skins

        //Unlock the already unlocked skins
        for (int i = 1; i < 10; i++)
        {
            if (PlayerPrefs.GetInt("UnlockedSkin"+i, 0)==1)
            {
                GameObject.Find("Skin" + i + "_mask").SetActive(false); //Make the mask disappear
            }
        }
    }

    public void UnlockRandomSkin()
    {
        if (nbrUnlockedSkins < 10) //Check if the player has not unlocked all the skins
        {
            int randomSkin = Random.Range(1, 10);
            while (PlayerPrefs.GetInt("UnlockedSkin" + randomSkin,0) == 1) //Until we find a locked skin
            {
                randomSkin = Random.Range(1, 10);
            }
    
            PlayerPrefs.SetInt("UnlockedSkin"+randomSkin,1); //Unlock the skin
            GameObject.Find("Skin" + randomSkin + "_mask").SetActive(false); //Make the mask disappear

            nbrUnlockedSkins++;
            PlayerPrefs.SetInt("NbrUnlockedSkins", nbrUnlockedSkins);//Increase counter of unlocked skins
        }

    }
}
