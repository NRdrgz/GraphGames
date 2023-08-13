using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonController : MonoBehaviour
{
    private bool soundOn;
    private int soundOnInt;
    public GameObject soundPicOn;
    public GameObject soundPicOff;

    // Start is called before the first frame update
    void Start()
    {
        soundOn = PlayerPrefs.GetInt("SoundOn", 1)==1;
        soundPicOn.SetActive(soundOn);
        soundPicOff.SetActive(!soundOn);
    }


    public void ClickSoundButton()
    {
        soundOn = !soundOn;
        soundOnInt = soundOn ? 1 : 0;
        PlayerPrefs.SetInt("SoundOn", soundOnInt);
        soundPicOn.SetActive(soundOn);
        soundPicOff.SetActive(!soundOn);

    }

}
