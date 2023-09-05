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
    public bool playerIsAlive;
    public float dumbTimer = 40f; //Timer at which enemies become Dumb

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
    public float spawnDelayRateSpikes; // Delay between two appearances of spikes
    public float spawnTimerSpikes = 0f;
    public int nbrBranchSpikes; // Number of spikes branches
    public int maxNbrRockets; //Maximum number of rockets




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
        screen[1].SetActive(false);//Hide the counter panel
        level = PlayerPrefs.GetInt("Level", 1);
        

        //LEVEL VARIABLES
        spawnDelayRateTiles = -0.15f*level+5.15f; //Start at 5 at level 1 and 2 at level 20;
        if (spawnDelayRateTiles<2)
        {
            spawnDelayRateTiles = 2;
        }

        spawnDelayRateRockets = -0.073f * level + 3.073f; //Start at 3 at level 1 and 1.6 at level 20;
        if (spawnDelayRateRockets < 1.6f)
        {
            spawnDelayRateRockets = 1.6f;
        }

        spawnDelayRateSpikes = -0.523f * level + 20.5f; //Start at 20 at level 1 and 10 at level 20;
        if (spawnDelayRateSpikes < 10f)
        {
            spawnDelayRateSpikes = 10f;
        }

        if (level<10)
        {
            nbrBranchSpikes = 1;
            maxNbrRockets = 2;
            
        }
        if (level >= 10)
        {
            nbrBranchSpikes = 2;
            maxNbrRockets = 4;
            
        }



        //LEVEL VARIABLES //

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

            //Update spawn timer Spikes
            spawnTimerSpikes -= Time.deltaTime;
            if (spawnTimerSpikes <= 0f)
            {
                CallCreateSpike();
                spawnTimerSpikes = spawnDelayRateSpikes;

            }

            //If character is the last survivor
            if (remainingEnemies==0 && playerIsAlive)
            {
                panelControllerWin.ShowPanel(); //Winning panel
                gameStarted = false;
            }


        }
    }

    public void StartGame()
    {
        
        screen[0].SetActive(false); //make the main screen disappear
        StartCoroutine(StartCounter()); //start the counter

        //So that it does not start immediatly
        spawnTimerSpikes = spawnDelayRateSpikes;

    }

    //Start the counter at the beginning of the game
    public System.Collections.IEnumerator StartCounter()
    {
        screen[1].SetActive(true);//Show the counter panel

        GameObject counter = GameObject.Find("Counter");

        counter.GetComponent<TextMeshProUGUI>().text = "3";
        iTween.PunchScale(counter, new Vector3(6, 6, 6), 1.0f);
        yield return new WaitForSeconds(1.1f); //Wait for 1s

        counter.GetComponent<TextMeshProUGUI>().text = "2";
        iTween.PunchScale(counter, new Vector3(6, 6, 6), 1.0f);
        yield return new WaitForSeconds(1.1f); //Wait for 1s

        counter.GetComponent<TextMeshProUGUI>().text = "1";
        iTween.PunchScale(counter, new Vector3(6, 6, 6), 1.0f);
        yield return new WaitForSeconds(1.1f); //Wait for 1s

        counter.GetComponent<TextMeshProUGUI>().text = "DODGE!";
        iTween.PunchScale(counter, new Vector3(6, 6, 6), 1.0f);
        yield return new WaitForSeconds(1.1f); //Wait for 1s

        screen[1].SetActive(false);//Remove the counter panel
        gameStarted = true; //start the game

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

    private void CallCreateSpike()
    {
        StartCoroutine(GetComponent<SpawnSpike>().CreateSpike());
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
