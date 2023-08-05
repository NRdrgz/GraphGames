using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRocket : MonoBehaviour
{

    //VARIABLES
    public float speed = 5f;
    public Transform explosionPrefab;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        // Move the rocket forward along its local y-axis
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Out")
        {
            Destroy(gameObject); //destroy rocket when it goes out
            
        }

        if (collision.gameObject.name == "MainPlayer")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            Vector3 position = contact.point;
            Instantiate(explosionPrefab, position, rotation);

            Destroy(GameObject.Find(collision.gameObject.name + "_pseudo")); //destroy the pseudo
            Destroy(collision.gameObject); //destroy player when it hits a rocket
            Destroy(gameObject);
            GridManager.Instance.playerIsAlive = false;
            GridManager.Instance.panelControllerLose.ShowPanel(); //make end screen appear
            

        }

        if (collision.gameObject.name.StartsWith("Enemy"))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            Vector3 position = contact.point;
            Instantiate(explosionPrefab, position, rotation);

            Destroy(GameObject.Find(collision.gameObject.name + "_pseudo")); //destroy the pseudo
            Destroy(collision.gameObject); //destroy enemy when it hits a rocket
            Destroy(gameObject);
            GridManager.Instance.remainingEnemies--;
            
        }

    }
}
