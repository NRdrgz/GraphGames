using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnRocket : MonoBehaviour
{
    public GameObject rocketPrefab;
    public int spawnPositionOffset; //offset to move the spawning compared to the grid
    private int numColumns;
    private int numRows;

    private void Start()
    {
        
    }

    private void Update()
    {
      
        
    }

    public System.Collections.IEnumerator CreateRocket()
    {
        // Determine a random side (top, bottom, left, or right) to spawn the rocket
        int sideIndex = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;
        Quaternion spawnRotation = Quaternion.identity;
        //(int numColumns, int numRows) = GetComponent<GridManager>().GetGridSize();
        (int numColumns, int numRows) = GridManager.Instance.GetGridSize();
        int spawnX = Random.Range(0, numColumns);
        int spawnZ = Random.Range(0, numRows);
        

        switch (sideIndex)
        {

            //grid is 0;-0.5 on bottom left and (numColumns-1);(numRows-1.5f) on top right, and Range is exclusive
            case 0: // Top side
                spawnPosition = new Vector3(spawnX, 0.25f, (numRows - 0.5f) + spawnPositionOffset);
                spawnRotation = Quaternion.Euler(-90f, 0f, 0f);
                //string message = "Spawning on top:" + spawnPosition;
                //Debug.Log(message);
                break;
            case 1: // Bottom side
                spawnPosition = new Vector3(spawnX, 0.25f, -0.5f - spawnPositionOffset);
                spawnRotation = Quaternion.Euler(90f, 0f, 0f);
                //string message1 = "Spawning on bottom:" + spawnPosition;
                //Debug.Log(message1);
                break;
            case 2: // Left side
                spawnPosition = new Vector3(0f-spawnPositionOffset, 0.25f, spawnZ - 0.5f);
                spawnRotation = Quaternion.Euler(0f, 0f, -90f);
                //string message2 = "Spawning on left:" + spawnPosition;
                //Debug.Log(message2);
                break;
            case 3: // Right side
                spawnPosition = new Vector3(numColumns + spawnPositionOffset, 0.25f, spawnZ - 0.5f);
                spawnRotation = Quaternion.Euler(0f, 0f, 90f);
                //string message3 = "Spawning on right:" + spawnPosition;
                //Debug.Log(message3);
                break;
        }

        //Blink the proper tiles and wait

        switch (sideIndex)
        {

            
            case 0: // Top side
                for (int row = 0; row < numRows; row++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX].GetComponentInChildren<Renderer>());
                }
                break;
            case 1: // Bottom side
                for (int row = 0; row < numRows; row++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX].GetComponentInChildren<Renderer>());
                }
                break;
            case 2: // Left side
                for (int col = 0; col < numColumns; col++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ, col].GetComponentInChildren<Renderer>());
                }
                break;
            case 3: // Right side
                for (int col = 0; col < numColumns; col++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ, col].GetComponentInChildren<Renderer>());
                }
                break;
        }


        yield return new WaitForSeconds(GetComponent<GridManager>().GetBlinkDuration()* GetComponent<GridManager>().GetBlinkTimes());
        // Instantiate the rocket prefab at the spawn position with the appropriate rotation
        GameObject rocket = Instantiate(rocketPrefab, spawnPosition, spawnRotation);

        //Increment the score
        if (GridManager.Instance.isPlayerAlive)
        {
            iTween.PunchScale(GridManager.Instance.score, new Vector3(2, 2, 2), 1.0f);
            int newScore = int.Parse(GridManager.Instance.score.GetComponent<TextMeshProUGUI>().text) + 1;
            GridManager.Instance.score.GetComponent<TextMeshProUGUI>().text = newScore.ToString();
        }
    }

    private void CallBlinkTile(Renderer tileRenderer)
    {
        StartCoroutine(GetComponent<GridManager>().BlinkTile(tileRenderer));
    }

}

