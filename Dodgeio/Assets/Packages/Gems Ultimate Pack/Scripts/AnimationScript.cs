using UnityEngine;
using System.Collections;


public class AnimationScript : MonoBehaviour {

    public bool isAnimated = false;

    public bool isRotating = false;
    public bool isFloating = false;
    public bool isScaling = false;

    public Vector3 rotationAngle;
    public float rotationSpeed;

    public float floatSpeed;
    private bool goingUp = true;
    public float floatRate;
    private float floatTimer;
   
    public Vector3 startScale;
    public Vector3 endScale;

    private bool scalingUp = true;
    public float scaleSpeed;
    public float scaleRate;
    private float scaleTimer;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

       
        
        if(isAnimated)
        {
            if(isRotating)
            {
                transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
            }

            if(isFloating)
            {
                floatTimer += Time.deltaTime;
                Vector3 moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
                transform.Translate(moveDir);

                if (goingUp && floatTimer >= floatRate)
                {
                    goingUp = false;
                    floatTimer = 0;
                    floatSpeed = -floatSpeed;
                }

                else if(!goingUp && floatTimer >= floatRate)
                {
                    goingUp = true;
                    floatTimer = 0;
                    floatSpeed = +floatSpeed;
                }
            }

            if(isScaling)
            {
                scaleTimer += Time.deltaTime;

                if (scalingUp)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, endScale, scaleSpeed * Time.deltaTime);
                }
                else if (!scalingUp)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, startScale, scaleSpeed * Time.deltaTime);
                }

                if(scaleTimer >= scaleRate)
                {
                    if (scalingUp) { scalingUp = false; }
                    else if (!scalingUp) { scalingUp = true; }
                    scaleTimer = 0;
                }
            }
        }
	}

    public IEnumerator OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.name == "MainPlayer")
        {

            //make the phone vibrate
            // Check if the device supports vibration
            /*if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                // Trigger a short vibration
                Handheld.Vibrate();
            }*/
            if (PlayerPrefs.GetInt("VibrateOn", 1) == 1)
            {
                Handheld.Vibrate();
            }

            iTween.MoveTo(gameObject, new Vector3(3f, 3.0f, 11f), 0.5f);//move the gem in upper right corner
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
            GemCounter.instance.IncreaseGems(GemCounter.instance.gemValue, "Game"); //increase counter


        }

        if (collision.gameObject.name.StartsWith("Enemy"))
        {
            Destroy(gameObject);

        }

    }

    
}
