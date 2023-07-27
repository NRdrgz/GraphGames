using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    
    public GameObject [] enemiesPrefab;
    private Vector3 enemyPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEnemy()
    {
        //Select a random skin
        GameObject enemyPrefab = enemiesPrefab[Random.Range(0, enemiesPrefab.Length)];

        //Select a random tile to spawn ont
        int row = Random.Range(0, GridManager.Instance.numColumns);
        int col = Random.Range(0, GridManager.Instance.numRows);
        enemyPosition = new Vector3(row, -0.038f, col - 0.5f);

        GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
        enemy.AddComponent<MovementEnemies>();
        //scale character same as spawner
        enemy.transform.localScale = transform.localScale;
        //rename game object
        enemy.name = "Enemy_" + GridManager.Instance.currentEnemies;

        GameObject.Find("PseudoSpawner").GetComponent<PseudoSpawner>().CreatePseudo(enemy, Color.red,
            PseudoRandom.instance.GetRandomPseudo()); //Create the pseudo to the enemy
        

    }
}
