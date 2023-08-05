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
    private int maxNbrRockets;
    private int nbrRockets;

    private void Start()
    {
        maxNbrRockets = GridManager.Instance.maxNbrRockets;
        
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

        //Determine the number of rockets
        nbrRockets = Random.Range(1, maxNbrRockets + 1); //No issues if the nbr of rocket is below the min size of the grid
        int remainingRockets = nbrRockets-1; //nbr of rockets to keep spawning
        int spawnCounter = 1;
        Vector3[] spawnPositionArray = new Vector3[nbrRockets]; //Array for the position of rockets
        
        

        switch (sideIndex)
        {

            //grid is 0;-0.5 on bottom left and (numColumns-1);(numRows-1.5f) on top right, and Range is exclusive
            case 0: // Top side
                spawnPosition = new Vector3(spawnX, 0.25f, (numRows - 0.5f) + spawnPositionOffset);
                spawnRotation = Quaternion.Euler(-90f, 0f, 0f);
                
                break;
            case 1: // Bottom side
                spawnPosition = new Vector3(spawnX, 0.25f, -0.5f - spawnPositionOffset);
                spawnRotation = Quaternion.Euler(90f, 0f, 0f);
                
                break;
            case 2: // Left side
                spawnPosition = new Vector3(0f-spawnPositionOffset, 0.25f, spawnZ - 0.5f);
                spawnRotation = Quaternion.Euler(0f, 0f, -90f);
                
                break;
            case 3: // Right side
                spawnPosition = new Vector3(numColumns + spawnPositionOffset, 0.25f, spawnZ - 0.5f);
                spawnRotation = Quaternion.Euler(0f, 0f, 90f);
                
                break;
        }
        spawnPositionArray[0] = spawnPosition;

        //Blink the proper tiles and wait

        switch (sideIndex)
        {

            
            case 0: // Top side
                for (int row = 0; row < numRows; row++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX].GetComponentInChildren<Renderer>());   
                }
                while (remainingRockets > 0 && spawnX + spawnCounter < numColumns) //Blink the tiles for additional rockets
                {
                    for (int row = 0; row < numRows; row++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX + spawnCounter].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x + spawnCounter, spawnPosition.y, spawnPosition.z);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                spawnCounter = 1; //if we hit the maximum of the grid, we try the other side
                while (remainingRockets > 0 && spawnX - spawnCounter >= 0) //Blink the tiles for additional rockets
                {
                    for (int row = 0; row < numRows; row++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX - spawnCounter].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x - spawnCounter, spawnPosition.y, spawnPosition.z);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                break;
            case 1: // Bottom side
                for (int row = 0; row < numRows; row++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX].GetComponentInChildren<Renderer>());
                }
                while (remainingRockets > 0 && spawnX + spawnCounter < numColumns) //Blink the tiles for additional rockets
                {
                    for (int row = 0; row < numRows; row++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX + spawnCounter].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x + spawnCounter, spawnPosition.y, spawnPosition.z);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                spawnCounter = 1; //if we hit the maximum of the grid, we try the other side
                while (remainingRockets > 0 && spawnX - spawnCounter >= 0) //Blink the tiles for additional rockets
                {
                    for (int row = 0; row < numRows; row++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, spawnX - spawnCounter].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x - spawnCounter, spawnPosition.y, spawnPosition.z);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                break;
            case 2: // Left side
                for (int col = 0; col < numColumns; col++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ, col].GetComponentInChildren<Renderer>());
                }
                while (remainingRockets > 0 && spawnZ + spawnCounter < numRows) //Blink the tiles for additional rockets
                {
                    for (int col = 0; col < numColumns; col++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ + spawnCounter, col].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z + spawnCounter);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                spawnCounter = 1; //if we hit the maximum of the grid, we try the other side
                while (remainingRockets > 0 && spawnZ - spawnCounter >= 0) //Blink the tiles for additional rockets
                {
                    for (int col = 0; col < numColumns; col++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ - spawnCounter, col].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z - spawnCounter);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                break;
            case 3: // Right side
                for (int col = 0; col < numColumns; col++)
                {
                    CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ, col].GetComponentInChildren<Renderer>());
                }
                while (remainingRockets > 0 && spawnZ + spawnCounter < numRows) //Blink the tiles for additional rockets
                {
                    for (int col = 0; col < numColumns; col++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ + spawnCounter, col].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z + spawnCounter);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                spawnCounter = 1; //if we hit the maximum of the grid, we try the other side
                while (remainingRockets > 0 && spawnZ - spawnCounter >= 0) //Blink the tiles for additional rockets
                {
                    for (int col = 0; col < numColumns; col++)
                    {
                        CallBlinkTile(GetComponent<GridManager>().GetGridCells()[spawnZ - spawnCounter, col].GetComponentInChildren<Renderer>());
                    }
                    spawnPositionArray[nbrRockets - remainingRockets] = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z - spawnCounter);
                    remainingRockets--;
                    spawnCounter++;
                    
                }
                break;
        }


        yield return new WaitForSeconds(GetComponent<GridManager>().GetBlinkDuration()* GetComponent<GridManager>().GetBlinkTimes());

        // Instantiate the rocket prefab at the spawn position with the appropriate rotation
        for (int i = 0; i < nbrRockets; i++)
        {
            Instantiate(rocketPrefab, spawnPositionArray[i], spawnRotation);
        }

    }

    private void CallBlinkTile(Renderer tileRenderer)
    {
        StartCoroutine(GetComponent<GridManager>().BlinkTile(tileRenderer));
    }

}

