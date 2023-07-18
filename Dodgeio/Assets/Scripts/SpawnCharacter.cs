using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{

    public GameObject characterPrefab;
    public float timeTrail;
    public AnimationCurve widthCurve;
    public Gradient colorGradient;
    public Material trailMaterial;
    private Vector3 characterPosition;



    void Start()
    {
   
        CreateCharacter();
        GridManager.Instance.isPlayerAlive = true;

        
    }

    void CreateCharacter()
    {
        //Select a random tile to spawn ont
        int row = Random.Range(0, GridManager.Instance.numColumns);
        int col = Random.Range(0, GridManager.Instance.numRows);
        characterPosition = new Vector3(row, -0.038f, col - 0.5f);
        GameObject character = Instantiate(characterPrefab, characterPosition, Quaternion.identity);
        character.AddComponent<MovementCharacter>();
        //scale character same as spawner
        character.transform.localScale = transform.localScale;
        //rename game object
        character.name = "MainPlayer";

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
