using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinUnlockGemBehavior : MonoBehaviour
{
    private int skinPrice;
    public GameObject skinPriceText;
    public int initialPrice;
    private int nbrUnlockedSkins;
    private int currentGemsMain;

    public float blinkDuration;
    public float animationDuration;
    public Color originalColor;
    public Color blinkColor;

    public void Awake()
    {
        //TODO: Change variables if more skins are added

        nbrUnlockedSkins = PlayerPrefs.GetInt("NbrUnlockedSkins", 0); //Retrieve the number of unlocked skins
        skinPrice = initialPrice * (1 + nbrUnlockedSkins); //Increase the price by 100% for each unlocked skins
        skinPriceText.GetComponent<TextMeshProUGUI>().text = skinPrice.ToString();
        

        //Unlock the already unlocked skins
        for (int i = 1; i < 10; i++)
        {
            if (PlayerPrefs.GetInt("UnlockedSkin"+i, 0)==1)
            {
                GameObject.Find("Skin" + i + "_mask").SetActive(false); //Make the mask disappear
            }
        }
    }


    public void UnlockRandomSkinWrapper() //Wrapper to call from the button
    {
        StartCoroutine(UnlockRandomSkin());
    }

    public System.Collections.IEnumerator UnlockRandomSkin()
    {
        currentGemsMain = PlayerPrefs.GetInt("CollectedGems", 0);
        if (nbrUnlockedSkins < 9 && currentGemsMain >= skinPrice) //Check if the player has not unlocked all the skins
        {
            int randomSkin = Random.Range(1, 10);
            while (PlayerPrefs.GetInt("UnlockedSkin" + randomSkin,0) == 1) //Until we find a locked skin
            {
                randomSkin = Random.Range(1, 10);
            }


            ///// Animation for unlocking the skin
            float elapsedTime = animationDuration;
            while (elapsedTime >= 0f)
            {
                for (int i = 1; i < 10; i++)
                {
                    if (PlayerPrefs.GetInt("UnlockedSkin" + i, 0) != 1) //If the skin is not unlocked
                    {
                        GameObject.Find("Skin" + i + "_mask").GetComponent<Image>().color = blinkColor;
                        yield return new WaitForSeconds(blinkDuration / 2f);

                        GameObject.Find("Skin" + i + "_mask").GetComponent<Image>().color = originalColor;
                        yield return new WaitForSeconds(blinkDuration / 2f);

                        elapsedTime -= blinkDuration;
                    }
                }
            }
            ///


            PlayerPrefs.SetInt("UnlockedSkin"+randomSkin,1); //Unlock the skin
            GameObject.Find("Skin" + randomSkin + "_mask").SetActive(false); //Make the mask disappear

            nbrUnlockedSkins++;
            GemCounter.instance.IncreaseGems(-skinPrice, "Main"); //Decrease number of gems

            skinPrice = initialPrice * (1 + nbrUnlockedSkins); //Increase the price by 100% for each unlocked skins
            skinPriceText.GetComponent<TextMeshProUGUI>().text = skinPrice.ToString();
            PlayerPrefs.SetInt("NbrUnlockedSkins", nbrUnlockedSkins);//Increase counter of unlocked skins
            
        }

    }


    // Make the mask blink
    public System.Collections.IEnumerator UnlockRandomAnimation()
    {
        float elapsedTime = animationDuration;
        while (elapsedTime >= 0f)
        {
            for (int i = 1; i < 10; i++)
            {
                if (PlayerPrefs.GetInt("UnlockedSkin" + i, 0) != 1) //If the skin is not unlocked
                {
                    GameObject.Find("Skin" + i + "_mask").GetComponent<Image>().color = blinkColor;
                    yield return new WaitForSeconds(blinkDuration / 2f);

                    GameObject.Find("Skin" + i + "_mask").GetComponent<Image>().color = blinkColor;
                    yield return new WaitForSeconds(blinkDuration / 2f);

                    elapsedTime -= blinkDuration;
                }
            }
        }

        
    }
}
