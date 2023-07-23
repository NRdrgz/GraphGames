using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    //VARIABLES
    private bool isSwiping = false;
    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float minimumSwipeDistance = 0f;
    private Animator animator;

    //LEVEL VARIABLES
    public float movementSpeed = 5f;

    public enum InputType
    {
        Swipe,
        Keyboard
    }

    public InputType inputType = InputType.Keyboard;



    void Start()
    {
        // Get the animator when spawning
        animator = GetComponent<Animator>();
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
        }
    }

    private bool IsValidMove(Vector3 position)
    {

        // For example, you can check if the position is within the grid boundaries or if the grid cell is occupied.
        if (position.z < -0.5 || position.x < 0 || position.z > GridManager.Instance.numRows - 1.5f || position.x > GridManager.Instance.numColumns - 1f)
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
            Destroy(gameObject); //destroy character
            GridManager.Instance.panelController.ShowPanel(); //make end screen appear

        }
    }
}

