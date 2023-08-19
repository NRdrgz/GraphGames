using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class HapticManager : MonoBehaviour
{
    

    public void Vibrate()
    {

        if (PlayerPrefs.GetInt("VibrateOn", 1) == 1)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Handheld.Vibrate();
            }

            if (Application.platform == RuntimePlatform.Android)
            {
                using (AndroidJavaObject unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                using (AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator"))
                {
                    if (vibrator.Call<bool>("hasVibrator"))
                    {
                        vibrator.Call("vibrate", 100);
                    }
                }
            }

        }



    }


}
