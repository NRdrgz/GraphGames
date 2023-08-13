using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinUnlockRVBehavior : MonoBehaviour
{
    private int skinPrice;
    public GameObject skinPriceText;
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
            if (PlayerPrefs.GetInt("UnlockedSkin" + i, 0) == 1)
            {
                GameObject.Find("Skin" + i + "_mask").SetActive(false); //Make the mask disappear
            }
        }
    }


    public void WatchRVUnlockSkin()
    {
        if (nbrUnlockedSkins < 9) //Check if the player has not unlocked all the skins
        {
            AdManager.instance.rvType = "Unlock Skin";
            AdManager.instance.ShowRewardedAd("Unlock Skin");
        }
    }

    public void UnlockRandomSkin()
    {
        
        if (nbrUnlockedSkins < 9 ) //Check if the player has not unlocked all the skins
        {
            int randomSkin = Random.Range(1, 10);
            while (PlayerPrefs.GetInt("UnlockedSkin" + randomSkin, 0) == 1) //Until we find a locked skin
            {
                randomSkin = Random.Range(1, 10);
            }

            PlayerPrefs.SetInt("UnlockedSkin" + randomSkin, 1); //Unlock the skin
            GameObject.Find("Skin" + randomSkin + "_mask").SetActive(false); //Make the mask disappear

            nbrUnlockedSkins++;
            skinPrice = initialPrice * (1 + nbrUnlockedSkins); //Increase the price by 100% for each unlocked skins
            skinPriceText.GetComponent<TextMeshProUGUI>().text = skinPrice.ToString();
            PlayerPrefs.SetInt("NbrUnlockedSkins", nbrUnlockedSkins);//Increase counter of unlocked skins
          
        }

    }
}
