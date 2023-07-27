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
    public GameObject[,] gridCells;
    public bool[,] gridCells_is_fallen;
    public bool gameStarted = false;
    public GameObject[] screen;
    public EndScreenControllerLose panelControllerLose;
    public EndScreenControllerWin panelControllerWin;
    private static GridManager instance;
    public GameObject gridCellPrefab; // Reference to a prefab for each grid cell
    public int nbrEnemies;
    public int remainingEnemies = 0;
    public int currentEnemies = 0;

    //LEVEL VARIABLES

    public int numRows;
    public int numColumns;
    public float blinkDuration;
    public int blinkTimes;
    private int level;
    public float spawnDelayRateTiles; // Delay between two drops of tiles
    public float spawnTimerTiles = 0f;
    public float spawnDelayRateRockets; // Delay between two appearances of rocket
    public float spawnTimerRockets = 0f;
    
    


    
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
        level = PlayerPrefs.GetInt("Level", 1);
        
        spawnDelayRateTiles = -0.15f*level+5.15f; //Start at 5 at level 1 and 2 at level 20;
        if (spawnDelayRateTiles<2)
        {
            spawnDelayRateTiles = 2;
        }

        spawnDelayRateRockets = -0.11f * level + 3.11f; //Start at 3 at level 1 and 1 at level 20;
        if (spawnDelayRateTiles < 1)
        {
            spawnDelayRateTiles = 1;
        }

    }


    void Update()
    {
        
        if (gameStarted)
        {
            //Spawn the enemies
            if (currentEnemies < nbrEnemies)
            {
                GameObject.Find("EnemySpawner").GetComponent<SpawnEnemies>().CreateEnemy();
                currentEnemies++;
                remainingEnemies++;
            }

            
            //Update spawn timer Rockets
            spawnTimerRockets -= Time.deltaTime;
            if (spawnTimerRockets <= 0f)
            {
                CallCreateRocket();
                spawnTimerRockets = spawnDelayRateRockets;

            }

            //Update spawn timer Tiles
            spawnTimerTiles -= Time.deltaTime;
            if (spawnTimerTiles <= 0f)
            {
                CallTileFall();
                spawnTimerTiles = spawnDelayRateTiles;

            }

            //If character is the last survivor
            if (remainingEnemies==0)
            {
                panelControllerWin.ShowPanel();
                gameStarted = false;
            }


        }
    }

    public void StartGame()
    {
        gameStarted = true;
        screen[0].SetActive(false); //make the main screen disappear
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
        if (tileRenderer.gameObject.tag != "Blinking") //to avoid the tile to stay blinking
        {
            tileRenderer.gameObject.tag = "Blinking"; //Tile the tile as blinking
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
            tileRenderer.gameObject.tag = "Untagged"; //Untag the tile
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

        }
    }

}
