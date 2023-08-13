using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{

    public AudioClip[] sfx;
    public AudioSource aSource;
    public static SfxManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySfxById(int id)
    {
        if (PlayerPrefs.GetInt("SoundOn", 1) == 1)
        {
            aSource.PlayOneShot(sfx[id]);
        }
    }
}
