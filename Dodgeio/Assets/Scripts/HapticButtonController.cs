using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticButtonController : MonoBehaviour
{
    private bool vibrateOn;
    private int vibrateOnInt;
    public GameObject vibratePicOn;
    public GameObject vibratePicOff;

    // Start is called before the first frame update
    void Start()
    {
        vibrateOn = PlayerPrefs.GetInt("VibrateOn", 1) == 1;
        vibratePicOn.SetActive(vibrateOn);
        vibratePicOff.SetActive(!vibrateOn);
    }


    public void ClickVibrateButton()
    {
        vibrateOn = !vibrateOn;
        vibrateOnInt = vibrateOn ? 1 : 0;
        PlayerPrefs.SetInt("VibrateOn", vibrateOnInt);
        vibratePicOn.SetActive(vibrateOn);
        vibratePicOff.SetActive(!vibrateOn);

    }
}
