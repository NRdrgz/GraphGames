using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpike : MonoBehaviour
{
    public Transform spikeDeathPrefab;
    public float spikeLifeTimer; //how long should the spike last
    public float speed;
    private bool hitEffectTriggered;
    public Transform hitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        hitEffectTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Make the Spike fell downward until it reaches tiles
        if (gameObject.transform.position.y > 0.4)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (gameObject.transform.position.y <= 0.4 && hitEffectTriggered != true)
        {
            Instantiate(hitPrefab, gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));//trigger the hit effect
            hitEffectTriggered = true;
        }


            //Update life span of Spike
            spikeLifeTimer -= Time.deltaTime;
        if (spikeLifeTimer <= 0f)
        {

            StartCoroutine(DestroySpike());

        }


    }


    public System.Collections.IEnumerator DestroySpike()
    {
        //Fade to 0 alpha in 2s
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, "time", 2f));

        yield return new WaitForSeconds(2f); //Wait for 2s
        Destroy(gameObject);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Out")
        {
            Destroy(gameObject); //destroy spike when it goes out

        }

        if (collision.gameObject.name == "MainPlayer")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            Vector3 position = new Vector3 (contact.point.x, 1, contact.point.z);
            Instantiate(spikeDeathPrefab, position, rotation);

            Destroy(GameObject.Find(collision.gameObject.name + "_pseudo")); //destroy the pseudo
            Destroy(collision.gameObject); //destroy player when it hits a spike
            Destroy(gameObject);
            GridManager.Instance.playerIsAlive = false;
            GridManager.Instance.panelControllerLose.ShowPanel(); //make end screen appear


        }

        if (collision.gameObject.name.StartsWith("Enemy"))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            Vector3 position = new Vector3(contact.point.x, 1, contact.point.z);
            Instantiate(spikeDeathPrefab, position, rotation);

            Destroy(GameObject.Find(collision.gameObject.name + "_pseudo")); //destroy the pseudo
            Destroy(collision.gameObject); //destroy enemy when it hits a spike
            Destroy(gameObject);
            GridManager.Instance.remainingEnemies--;

        }

    }
}
