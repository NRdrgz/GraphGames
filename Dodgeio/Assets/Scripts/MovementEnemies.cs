using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemies : MonoBehaviour
{   
    public float movementSpeed = 5f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    private float moveTimer = 0f;
    public float checkInterval = 0.2f; //Check interval for red tiles
    private float checkTimer = 0f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Animator animator;
    private float dumbTimer;

    public enum MoveType
    {
        Random,
        Smart
    }

    public MoveType moveType = MoveType.Smart;


    // Start is called before the first frame update
    void Start()
    {
        // Get the animator when spawning
        animator = GetComponent<Animator>();
        moveTimer = Random.Range(minWaitTime, maxWaitTime);
        dumbTimer = GridManager.Instance.dumbTimer;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Update movement
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }

        //Update movement timer
        moveTimer -= Time.deltaTime;
        if (!isMoving && moveTimer <= 0f)
        {
            //Reset timer
            moveTimer = Random.Range(minWaitTime, maxWaitTime);

            if (moveType == MoveType.Smart)
            {
                DecideSmartDirection(false);
            }
            else
            {
                DecideRandomDirection();
            }
            
        }

        if (moveType == MoveType.Smart)
        {
            //Update check timer
            checkTimer -= Time.deltaTime;
            if (!isMoving && checkTimer <= 0f)
            {
                //Reset timer
                checkTimer = checkInterval;
                CheckForRedTile();
            }

        }

        //Become dumb after dumbTimer is over
        //Update dumb timer
        dumbTimer -= Time.deltaTime;
        if (dumbTimer <= 0f)
        {
            //Reset timer
            dumbTimer = GridManager.Instance.dumbTimer; ;
            moveType = MoveType.Random;
        }

        //Handle animations
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("Speed", movementSpeed);

    }


    private void DecideRandomDirection()
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
    
    private void DecideSmartDirection(bool needToMove)
    {
        //Scan the surroundings
        /*Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0.5f, -1f, 0f)*5, Color.black, 3f); //Right ray
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(-0.5f, -1f, 0f) * 5, Color.red, 3f); //Left ray
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0f, -1f, 0.5f) * 5, Color.blue, 3f); //Up ray
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0f, -1f, -0.5f) * 5, Color.yellow, 3f); //Down ray
        */
        int[] moveOrder = new int[] { 0, 1, 2, 3 }; //Move order to check
        moveOrder = ShuffleArray(moveOrder); //Shuffle the array
        RaycastHit hit;
        bool hasMoved = false;
        Vector3[] movePosibilities = new Vector3[] {Vector3.right, Vector3.left, Vector3.back, Vector3.forward};

        // Iterate through the shuffled array using a for loop
        for (int i = 0; i < moveOrder.Length; i++)
        {
            
            if (moveOrder[i] == 0
                && Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0.5f, -1f, 0f) * 5, out hit, 5f)) //right
            {
                /*Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
                new Vector3(0.5f, -1f, 0f) * 5, Color.black, 3f);
                Debug.Log("On right there is " + hit.collider.gameObject.name + " with Tag " + hit.collider.gameObject.tag);*/
                //Check if the object is a valid Tile
                if (hit.collider.gameObject.name == "Tile"
                && hit.collider.gameObject.tag != "Blinking")
                {
                    //Debug.Log("Nice tile on right");
                    hasMoved = true;
                    MoveCharacter(Vector3.right);
                    break;
                }

            }

            else if (moveOrder[i] == 1
                && Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0f, -1f, 0.5f) * 5, out hit, 5f)) //up
            {
                /*Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0f, -1f, 0.5f) * 5, Color.blue, 3f);
                Debug.Log("On up there is " + hit.collider.gameObject.name + " with Tag " + hit.collider.gameObject.tag);*/
                //Check if the object is a valid Tile
                if (hit.collider.gameObject.name == "Tile"
                && hit.collider.gameObject.tag != "Blinking")
                {
                    //Debug.Log("Nice tile on up");
                    hasMoved = true;
                    MoveCharacter(Vector3.forward);
                    break;
                }

            }

            else if (moveOrder[i] == 2
                && Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(-0.5f, -1f, 0f) * 5, out hit, 5f)) //left
            {
                /*Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(-0.5f, -1f, 0f) * 5, Color.red, 3f);
                Debug.Log("On left there is " + hit.collider.gameObject.name + " with Tag " + hit.collider.gameObject.tag);*/
                //Check if the object is a valid Tile
                if (hit.collider.gameObject.name == "Tile"
                && hit.collider.gameObject.tag != "Blinking")
                {
                    //Debug.Log("Nice tile on left");
                    hasMoved = true;
                    MoveCharacter(Vector3.left);
                    break;
                }

            }

            else if (moveOrder[i] == 3
                && Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0f, -1f, -0.5f) * 5, out hit, 5f)) //back
            {
                /*Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),
            new Vector3(0f, -1f, -0.5f) * 5, Color.yellow, 3f);
                Debug.Log("On back there is " + hit.collider.gameObject.name + " with Tag " + hit.collider.gameObject.tag);*/
                //Check if the object is a valid Tile
                if (hit.collider.gameObject.name == "Tile"
                && hit.collider.gameObject.tag != "Blinking")
                {
                    //Debug.Log("Nice tile on back");
                    hasMoved = true;
                    MoveCharacter(Vector3.back);
                    break;
                }

            }

            
        }

        if (needToMove == true && hasMoved == false) //if have not moved before, move randomly
        {
            MoveCharacter(movePosibilities[Random.Range(0, 4)]);
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
        if (position.z < -0.6 || position.x < -0.1 || position.z > GridManager.Instance.numRows - 1.4f || position.x > GridManager.Instance.numColumns - 0.9f)
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

    private void CheckForRedTile()
    {
        // Raycast downward to detect if the character is standing on a red tile
        RaycastHit hit;
        
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), Vector3.down, out hit, 5f))
        {
            // Check if the object is a redtile
            if (hit.collider.gameObject.name == "Tile"
                && hit.collider.gameObject.tag == "Blinking")
            {
                // Character is standing on a red tile, move character
                //Debug.Log("Character is standing on a red tile!");
                if (!isMoving)
                {
                    DecideSmartDirection(true);
                }
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        //If the enemy fall out
        if (collision.gameObject.name == "Out")
        {

            Destroy(GameObject.Find(gameObject.name + "_pseudo")); //destroy the pseudo
            Destroy(gameObject); //destroy ennemy

            GridManager.Instance.remainingEnemies--;
        }

    }

    private int[] ShuffleArray(int[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = array[k];
            array[k] = array[n];
            array[n] = value;
        }

        return array;
    }





}
