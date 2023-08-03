using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpike : MonoBehaviour
{
    public GameObject spikePrefab;
    private int numColumns;
    private int numRows;
    private int nbrBranchSpikes;
    private int directionDiagonal;
    private (int, int)[] selectedSpawnPos; //Array with all the rows and cols to spawn spikes on
    

    // Start is called before the first frame update
    void Start()
    {
        numColumns = GridManager.Instance.numColumns;
        numRows = GridManager.Instance.numRows;
        nbrBranchSpikes = GridManager.Instance.nbrBranchSpikes;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public System.Collections.IEnumerator CreateSpike()
    {
        //Initial position finder
        //Find a random not fallen cell
        int rowInit = Random.Range(0, numRows);
        int colInit = Random.Range(0, numColumns);
        Quaternion spawnRotation = Quaternion.identity;

        while (GridManager.Instance.gridCells_is_fallen[rowInit, colInit] == true) //test cells until we have on which have not fallen yet
        {
            rowInit = Random.Range(0, numRows);
            colInit = Random.Range(0, numColumns);
        }


        if (nbrBranchSpikes == 1) //Only do a diagonal
        {
            directionDiagonal = Random.Range(0, 2); //Random direction for the diagonal

            int rowDown = rowInit;
            int colDown = colInit;

            int rowUp = rowInit + 1;
            int colUp = colInit + 1;

            int counterPos = 0;
            selectedSpawnPos = new (int, int)[numColumns*2];

            switch (directionDiagonal)
            {

                case 0: //From down left to up right

                   

                    while (rowDown >= 0 && colDown >= 0)
                    {
                        
                        selectedSpawnPos[counterPos] = (rowDown, colDown);
                        counterPos++;
                        rowDown--;
                        colDown--;
                    }
                    while (rowUp < numRows && colUp < numColumns)
                    {

                        
                        selectedSpawnPos[counterPos] = (rowUp, colUp);
                        counterPos++;
                        rowUp++;
                        colUp++;

                    }


                    break;

                case 1: //From up left to down right

                   

                    rowUp = rowInit + 1;
                    colUp = colInit - 1;

                    while (rowDown >= 0 && colDown < numColumns)
                    {
                        
                        selectedSpawnPos[counterPos] = (rowDown, colDown);
                        counterPos++;
                        rowDown--;
                        colDown++;
                    }
                    while (rowUp < numRows && colUp >= 0)
                    {
                    
                        selectedSpawnPos[counterPos] = (rowUp, colUp);
                        counterPos++;
                        rowUp++;
                        colUp--;

                    }

                    break;

            }

            for (int i = 0; i < counterPos; i++)
            {
                (int row, int col) = selectedSpawnPos[i];
                CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, col].GetComponentInChildren<Renderer>());

            }

            yield return new WaitForSeconds(GetComponent<GridManager>().GetBlinkDuration() * GetComponent<GridManager>().GetBlinkTimes());

            for (int i = 0; i < counterPos; i++)
            {
                (int row, int col) = selectedSpawnPos[i];
                Vector3 spawnPos = new Vector3(col, 10f, row - 0.5f);
                Instantiate(spikePrefab, spawnPos, spawnRotation);
                yield return new WaitForSeconds(0.05f); //Effect for all spikes not to fall at the same time
            }

        }


        else //Do a cross
        {
            int rowDown = rowInit;
            int colDown = colInit;

            int rowUp = rowInit + 1;
            int colUp = colInit + 1;

            int counterPos = 0;
            selectedSpawnPos = new (int, int)[numColumns * 4];

            //From down left to up right
            while (rowDown >= 0 && colDown >= 0)
            {

                selectedSpawnPos[counterPos] = (rowDown, colDown);
                counterPos++;
                rowDown--;
                colDown--;
            }
            while (rowUp < numRows && colUp < numColumns)
            {


                selectedSpawnPos[counterPos] = (rowUp, colUp);
                counterPos++;
                rowUp++;
                colUp++;

            }


            rowDown = rowInit - 1;
            colDown = colInit + 1;
            rowUp = rowInit + 1;
            colUp = colInit - 1;
            //From up left to down right
            while (rowDown >= 0 && colDown < numColumns)
            {

                selectedSpawnPos[counterPos] = (rowDown, colDown);
                counterPos++;
                rowDown--;
                colDown++;
            }
            while (rowUp < numRows && colUp >= 0)
            {

                selectedSpawnPos[counterPos] = (rowUp, colUp);
                counterPos++;
                rowUp++;
                colUp--;

            }


            for (int i = 0; i < counterPos; i++)
            {
                (int row, int col) = selectedSpawnPos[i];
                CallBlinkTile(GetComponent<GridManager>().GetGridCells()[row, col].GetComponentInChildren<Renderer>());

            }

            yield return new WaitForSeconds(GetComponent<GridManager>().GetBlinkDuration() * GetComponent<GridManager>().GetBlinkTimes());

            for (int i = 0; i < counterPos; i++)
            {
                (int row, int col) = selectedSpawnPos[i];
                Vector3 spawnPos = new Vector3(col, 10f, row - 0.5f);
                Instantiate(spikePrefab, spawnPos, spawnRotation);
                yield return new WaitForSeconds(0.05f); //Effect for all spikes not to fall at the same time
            }

        }


    }

    private void CallBlinkTile(Renderer tileRenderer)
    {
        StartCoroutine(GetComponent<GridManager>().BlinkTile(tileRenderer));
    }
}
