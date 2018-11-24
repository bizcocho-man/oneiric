using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player_AnimatorController))]
public class Player_MovementController : MonoBehaviour
{
    [HideInInspector] public SO_PlayerData currentPlayerData;

    // Components
    private Rigidbody rigidBody;
    private Player_AnimatorController playerAnimatorController;
    [SerializeField] private GameObject feet;

    // Input
    private float horizontalAxis;
    private float inputJump;

    // Movement
    private Vector3 currentVelocity;
    private bool isFacingRight = true;
    private bool canJump;
    private bool canLand = true;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isGrabbing;
    [HideInInspector] public bool isOnGround;
    [HideInInspector] public bool isMovementDisabled;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerAnimatorController = GetComponent<Player_AnimatorController>();
    }

    void Update()
    {
        GetInput();
        DetectJumping();
        DetectDirection();
        CalculateCurrentVelocity();
        UpdateAnimatorVariables();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = currentVelocity;
    }

    private void GetInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        inputJump = Input.GetAxisRaw("Vertical");
    }

    private void DetectJumping()
    {
        if (inputJump > 0 && !isJumping && isOnGround)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void DetectDirection()
    {
        if (isGrabbing || !canMove)
        {
            return;
        }

        if (horizontalAxis > 0.0f && !isFacingRight)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        else if (horizontalAxis < 0.0f && isFacingRight)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
    }

    private void CalculateCurrentVelocity()
    {
        // Set CurrentVelocity based on input
        Vector3 newVelocity = rigidBody.velocity;

        newVelocity.x = canMove ? horizontalAxis * currentPlayerData.movementSpeed * Time.deltaTime : 0.0f;
        newVelocity.x = isGrabbing ? newVelocity.x / 2f : newVelocity.x;
        
        //newVelocity.y = isJumping ? newVelocity.y = jumpForce : newVelocity.y + acceleration * Time.deltaTime;

        if (!isGrabbing)
        {
            // Jumping
            if (isJumping)
            {
                newVelocity.y = currentPlayerData.jumpSpeed;
            }
            // Falling
            else if (newVelocity.y < currentPlayerData.jumpSpeed)
            {
                newVelocity.y -= currentPlayerData.fallingAcceleraion * Time.deltaTime;

                if (newVelocity.y < -currentPlayerData.maxFallingSpeed)
                {
                    newVelocity.y = -currentPlayerData.maxFallingSpeed;
                }
            }
        }

        /*if (isJumping)
        {
            newVelocity -= Vector3.up * Physics.gravity.y * (jumpForce) * Time.deltaTime;
        }
        else if (newVelocity.y < jumpForce)
        {
            newVelocity += Vector3.up * Physics.gravity.y * (fallForce) * Time.deltaTime;
        }*/

        currentVelocity = newVelocity; 
    }

    private void UpdateAnimatorVariables()
    {
        if (playerAnimatorController == null)
        {
            return;
        }

        playerAnimatorController.movementSpeed = Mathf.Abs(horizontalAxis);
        playerAnimatorController.animatorSpeed = currentPlayerData.animatorSpeedMultiplier;
        playerAnimatorController.isOnGround = isOnGround;
        playerAnimatorController.canMove = canMove;
        playerAnimatorController.canJump = canJump;
        playerAnimatorController.canLand = canLand;
        playerAnimatorController.isJumping = isJumping;
    }

    //public void Event_CanMove(int value /* 0 = false, 1 = true */)
    //{
    //    canMove = value == 0 ? false : true;
    //}

    //public void Event_CanLand(int value /* 0 = false, 1 = true */)
    //{
    //    canLand = value == 0 ? false : true;
    //}

    //public void Event_IsJumping(int value /* 0 = false, 1 = true */)
    //{
    //    isJumping = value == 0 ? false : true;
    //}
}
