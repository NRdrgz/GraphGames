using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    public GameObject gemtext;
    public int currentGems = 0;
    public static GemCounter instance;

    private void Awake()
    {
        instance = this;
        currentGems = PlayerPrefs.GetInt("CollectedGems");
    }

    // Start is called before the first frame update
    void Start()
    {
        gemtext.GetComponent<TextMeshProUGUI>().text = currentGems.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseGems(int v)
    {
        currentGems = currentGems + v;
        PlayerPrefs.SetInt("CollectedGems", currentGems);
        iTween.PunchScale(gemtext, new Vector3(2, 2, 2), 1.0f);
        gemtext.GetComponent<TextMeshProUGUI>().text = currentGems.ToString();
    }
}
