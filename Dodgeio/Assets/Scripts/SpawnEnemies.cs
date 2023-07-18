using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public int nbrEnemies;
    public GameObject [] enemiesPrefab;
    private Vector3 enemyPosition;


    // Start is called before the first frame update
    void Start()
    {
        CreateEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        for (int i = 0; i < nbrEnemies; i++)
        {
            //Select a random skin
            GameObject enemyPrefab = enemiesPrefab[Random.Range(0, enemiesPrefab.Length)];

            //Select a random tile to spawn ont
            int row = Random.Range(0, GridManager.Instance.numColumns);
            int col = Random.Range(0, GridManager.Instance.numRows);
            enemyPosition = new Vector3(row, -0.038f, col-0.5f);
            
            GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
            enemy.AddComponent<MovementEnemies>();
            //scale character same as spawner
            enemy.transform.localScale = transform.localScale;
            //rename game object
            enemy.name = "Enemy"+i;
        }

                
        

    }
}
