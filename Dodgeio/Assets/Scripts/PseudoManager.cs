using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PseudoManager : MonoBehaviour
{
    Transform mainCam;
    Transform wolrdSpaceCanvas;
    private GameObject target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform; //Reference camera so the text always faces it
        wolrdSpaceCanvas = GameObject.Find("WorldCanvas").transform;

        transform.SetParent(wolrdSpaceCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position); // look at camera

        if (target != null) //Follow the target
        {
            transform.position = target.transform.position + offset;
        }
    }

    public void AssignPseudo(GameObject target_designed, Color targetColor, string targetText)
    {
        target = target_designed;
        gameObject.name = target_designed.name + "_pseudo";
        gameObject.GetComponent<TextMeshPro>().color = targetColor;
        gameObject.GetComponent<TextMeshPro>().text = targetText;
    }
}
