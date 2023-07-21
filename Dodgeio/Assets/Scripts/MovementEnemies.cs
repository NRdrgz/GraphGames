using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemies : MonoBehaviour
{
    public string[] moveType = { "Random", "Smart" };
    public string selectedMoveType = "Random";
    public float movementSpeed = 5f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        // Get the animator when spawning
        animator = GetComponent<Animator>();
        StartCoroutine(MoveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }

        //Handle animations
        animator.SetBool("isMoving", isMoving);

    }

    private System.Collections.IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // Wait for a random time before moving again
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // Move the character
            if (!isMoving)
            {
                MoveEnemy();
            }
        }

    }


    private void MoveEnemy()
    {
        DecideDirection();
    }

    private void DecideDirection()
    {
        if (selectedMoveType == "Random")
        {
            int sideIndex = Random.Range(0, 4);
            switch (sideIndex)
            {
                case 0: //right
                    MoveCharacter(Vector3.right);
                    break;
                case 1: //up
                    MoveCharacter(Vector3.forward);
                    break;
                case 2: //left
                    MoveCharacter(Vector3.left);
                    break;
                case 3: //down
                    MoveCharacter(Vector3.back);
                    break;

            }
        }
    }

    private void MoveCharacter(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction;
        // Calculate the target rotation based on the move direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Rotate the character immediately to face the move direction
        transform.rotation = targetRotation;

        // Check if the new position is within the grid boundaries
        if (IsValidMove(newPosition))
        {
            targetPosition = newPosition;
            isMoving = true;
        }
    }


    private bool IsValidMove(Vector3 position)
    {

        // For example, you can check if the position is within the grid boundaries or if the grid cell is occupied.
        if (position.z < -0.5 || position.x < 0 || position.z > GridManager.Instance.numRows-1.5f || position.x > GridManager.Instance.numColumns - 1f)
        {
            return false;
        }


        //Prevent character from moving if he's falling
        if (gameObject.GetComponent<Rigidbody>().velocity.y < -0.01) //Character is falling
        {
            return false;
        }


        // Return true if the move is valid, otherwise return false.
        return true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //If the players fall out
        if (collision.gameObject.name == "Out")
        {
            Destroy(gameObject); //destroy ennemy

        }
    }


}
