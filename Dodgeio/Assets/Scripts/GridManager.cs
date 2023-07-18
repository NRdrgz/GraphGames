using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    //VARIABLES
    
    public Color lightColor;
    public Color darkColor;
    public Color blinkColor;
    private GameObject[,] gridCells;
    private bool[,] gridCells_is_fallen;
    public bool gameStarted = false;
    public GameObject[] tutos;
    public EndScreenController panelController;
    private static GridManager instance;
    public GameObject gridCellPrefab; // Reference to a prefab for each grid cell
    public GameObject score;
    public bool isPlayerAlive = true;

    //LEVEL VARIABLES

    public int numRows;
    public int numColumns;
    public float blinkDuration;
    public int blinkTimes;
    public float spawnDelayInit; // Delay to make first rocket appear
    public float spawnDelayRate; // Delay between two appearances of rocket
    
    


    
    // Make the GridManager and Instance to call it elsewhere
    private void Awake()
    {
        instance = this;
        
    }

    public static GridManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        GenerateGrid();
        score = GameObject.Find("ScoreText"); //retrieve the score
        

        // Spawn the rocket on a random side of the grid
        InvokeRepeating("CallCreateRocket", spawnDelayInit, spawnDelayRate);

        //Make tile fall
        InvokeRepeating("CallTileFall", spawnDelayInit, spawnDelayRate);
        
    }


    void Update()
    {
        //to remove the tutorial frame
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            gameStarted = true;
            tutos[0].SetActive(false);
        }

    }

    void GenerateGrid()
    {

        gridCells = new GameObject[numRows, numColumns];
        gridCells_is_fallen = new bool[numRows, numColumns];
        

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                // Calculate the position for each grid cell
                Vector3 position = new Vector3(col, 0, row);

                // Instantiate the grid cell prefab at the calculated position
                GameObject cell = Instantiate(gridCellPrefab, position, Quaternion.identity, transform);

                //Alternate the color one every two cell
                Renderer tileRenderer = cell.GetComponentInChildren<Renderer>();
                if ((row + col) % 2 == 0)
                    tileRenderer.material.color = lightColor;
                else
                    tileRenderer.material.color = darkColor;

                //get ref of the cell in an array for future use
                gridCells[row, col] = cell;
                gridCells_is_fallen[row, col] = false; //initialize the array as all cells have not fallen
                
            }
        }


    }


    public (int, int) GetGridSize()
    {
        return ((int)numColumns, (int)numRows);
    }

    public GameObject[,] GetGridCells()
    {
        return gridCells;
    }

    public float GetBlinkDuration()
    {
        return blinkDuration;
    }

    public int GetBlinkTimes()
    {
        return blinkTimes;
    }

    private void CallCreateRocket()
    {
        StartCoroutine(GetComponent<SpawnRocket>().CreateRocket());
    }

    private void CallTileFall()
    {
        int row = Random.Range(0, numRows);
        int col = Random.Range(0, numColumns);
        while (gridCells_is_fallen[row, col]==true) //test cells until we have on which have not fallen yet
        {
            row = Random.Range(0, numRows);
            col = Random.Range(0, numColumns);
        }
        
        StartCoroutine(MakeGridCellFall(row, col));
    }

    //function to blink tiles
    public System.Collections.IEnumerator BlinkTile(Renderer tileRenderer)
    {
        // Blink the tile color between original and blink colors
        
        Color originalColor = tileRenderer.material.color;
        for (int i = 0; i < blinkTimes; i++)
        {
            float elapsedTime = 0f;
            while (elapsedTime < blinkDuration)
            {
                tileRenderer.material.color = blinkColor;
                yield return new WaitForSeconds(blinkDuration / 2f);
                tileRenderer.material.color = originalColor;
                yield return new WaitForSeconds(blinkDuration / 2f);
                elapsedTime += blinkDuration;
            }
        }
    }


    public System.Collections.IEnumerator MakeGridCellFall(int row, int col)
    {
        GameObject cell = gridCells[row, col];
        Rigidbody cellRigidbody = cell.GetComponentInChildren<Rigidbody>();
        if (cellRigidbody != null)
        {
            StartCoroutine(BlinkTile(cell.GetComponentInChildren<Renderer>())); //Blink the tile
            yield return new WaitForSeconds(blinkDuration * blinkTimes);//Wait for the tile to blink
            cellRigidbody.isKinematic = false; //Make the tile fall
            gridCells_is_fallen[row, col] = true; //Reference the fall

            //Increment the score
            if (GridManager.Instance.isPlayerAlive)
            {
                iTween.PunchScale(GridManager.Instance.score, new Vector3(2, 2, 2), 1.0f);
                int newScore = int.Parse(GridManager.Instance.score.GetComponent<TextMeshProUGUI>().text) + 1;
                GridManager.Instance.score.GetComponent<TextMeshProUGUI>().text = newScore.ToString();
            }
        }
    }

}
