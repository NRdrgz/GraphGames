using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlockRVBehavior : MonoBehaviour
{
    public GameObject shop;

    public void SelectSkin(int skinId)
    {
        Debug.Log("Clicked on Skin Selector Gem" + skinId);
        //TODO 
        shop.SetActive(false); //remove the shop

    }
}
