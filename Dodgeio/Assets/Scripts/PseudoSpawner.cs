using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PseudoSpawner : MonoBehaviour
{
    public GameObject PseudoPrefab;

    

    public void CreatePseudo(GameObject target_designed, Color targetColor, string targetText)
    {
        GameObject newPseudo = Instantiate(PseudoPrefab, new Vector3(0,0,0), new Quaternion()); //create game object pseudo
        newPseudo.GetComponent<PseudoManager>().AssignPseudo(target_designed, targetColor, targetText);
        
    }

}
