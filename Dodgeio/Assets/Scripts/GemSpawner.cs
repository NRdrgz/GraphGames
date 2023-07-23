using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    public int maxGems = 10;
    public float spawnInterval = 5f;

    private int gemsSpawned = 0;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gemsSpawned < maxGems && GridManager.Instance.gameStarted)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnGem();
                timer = 0f;
            }
        }
    }

    private void SpawnGem()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject gem = Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        gem.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        gemsSpawned++;
    }


    private Vector3 GetRandomSpawnPosition()
    {
        (int numColumns, int numRows) = GridManager.Instance.GetGridSize();
        int row = Random.Range(0, numRows);
        int col = Random.Range(0, numColumns);
        while (GridManager.Instance.gridCells_is_fallen[row, col] == true) //test cells until we have on which have not fallen yet
        {
            row = Random.Range(0, numRows);
            col = Random.Range(0, numColumns);
        }
        
        float x = GridManager.Instance.gridCells[row, col].transform.position.x;
        float y = 0.7f; 
        float z = GridManager.Instance.gridCells[row, col].transform.position.z - 0.5f;
        return new Vector3(x, y, z);
    }

    
}
