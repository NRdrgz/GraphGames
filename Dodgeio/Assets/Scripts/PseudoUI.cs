using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PseudoUI : MonoBehaviour
{
    private string pseudo;

    // Start is called before the first frame update
    void Start()
    {
        pseudo = PlayerPrefs.GetString("PlayerPseudo", "Player");
        gameObject.GetComponent<TMP_InputField>().text = pseudo;
    }

    public void UpdatePseudo()
    {
        pseudo = gameObject.GetComponent<TMP_InputField>().text;
        PlayerPrefs.SetString("PlayerPseudo", pseudo);

        GameObject.Find("MainPlayer_pseudo").GetComponent<TextMeshPro>().text = pseudo; //update the pseudo of the player
    }
}
