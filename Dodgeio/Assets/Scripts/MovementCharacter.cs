using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    //VARIABLES
    private bool isSwiping = false;
    public float movementSpeed;
    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float minimumSwipeDistance = 0f;
    private Animator animator;

    //LEVEL VARIABLES
    public float initialMovementSpeed = 10f;
    

    public enum InputType
    {
        Swipe,
        Keyboard
    }

    public InputType inputType = InputType.Swipe;



    void Start()
    {
        // Get the animator when spawning
        animator = GetComponent<Animator>();

        //Change input type if on desktop editor
        if(Application.isEditor)
        {
            inputType = InputType.Keyboard;
        }

        movementSpeed = initialMovementSpeed * (1f + PlayerPrefs.GetInt("SpeedLevel", 1)/10f); //Update movement speed with speed Level from Incr button
    }



    void Update()
    {

        if (inputType == InputType.Swipe)
        {
            HandleSwipeInput();
        }
        else if (inputType == InputType.Keyboard)
        {
            HandleKeyboardInput();
        }
        


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
        animator.SetFloat("Speed", movementSpeed);
    }



    private void HandleKeyboardInput()
    {
        if (!isMoving)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (horizontalInput > 0f) // Right
            {
                MoveCharacter(Vector3.right);
            }
            else if (verticalInput > 0f) // Up
            {
                MoveCharacter(Vector3.forward);
            }
            else if (horizontalInput < 0f) // Left
            {
                MoveCharacter(Vector3.left);
            }
            else if (verticalInput < 0f) // Down
            {
                MoveCharacter(Vector3.back);
            }
        }
    }


    private void HandleSwipeInput()
    {
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                swipeStartPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isSwiping)
                {
                    swipeEndPosition = Input.mousePosition;
                    CalculateSwipeDirection();
                }

                isSwiping = false;
            }


        }
    }


    private void CalculateSwipeDirection()
    {
        Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
        float swipeMagnitude = swipeDirection.magnitude;

        if (swipeMagnitude >= minimumSwipeDistance)
        {
            swipeDirection.Normalize();
            float angle = Mathf.Atan2(swipeDirection.y, swipeDirection.x) * Mathf.Rad2Deg;

            if (angle >= -45 && angle < 45) // Right
            {
                MoveCharacter(Vector3.right);
            }
            else if (angle >= 45 && angle < 135) // Up
            {
                MoveCharacter(Vector3.forward);
            }
            else if (angle >= 135 || angle < -135) // Left
            {
                MoveCharacter(Vector3.left);
            }
            else if (angle >= -135 && angle < -45) // Down
            {
                MoveCharacter(Vector3.back);
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
            SfxManager.instance.PlaySfxById(0);
        }
    }

    private bool IsValidMove(Vector3 position)
    {

        // We do not put exactly -0.5 or 0 to avoid rounding numbers issue
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

    private void OnCollisionEnter(Collision collision)
    {
        //If the players fall out
        if (collision.gameObject.name == "Out")
        {
            Destroy(GameObject.Find(gameObject.name + "_pseudo")); //destroy the pseudo
            Destroy(gameObject); //destroy character
            GridManager.Instance.playerIsAlive = false;

            

            GridManager.Instance.panelControllerLose.ShowPanel(); //make end screen appear

        }
    }
}

