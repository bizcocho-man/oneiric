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

    // Input
    private float horizontalAxis;
    private int horizontalAxisRaw;
    private float inputJump;

    // Movement
    private Vector3 currentVelocity;
    private bool isFacingRight = true;
    private bool canJump;
    private bool canLand = true;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canRotate = true;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isGrabbing;
    [HideInInspector] public bool isOnGround;
    [HideInInspector] public bool isMovementBlocked;
    [HideInInspector] public bool isAgainstObstacle;
    public float movementSpeedMultiplier = 1.0f;

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
        horizontalAxisRaw = (int)Input.GetAxisRaw("Horizontal");
        inputJump = Input.GetAxisRaw("Vertical");

        canMove = !isMovementBlocked && !isAgainstObstacle;
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
        if (isGrabbing || !canRotate)
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

        newVelocity.x = canMove ? horizontalAxis * currentPlayerData.movementSpeed * movementSpeedMultiplier * Time.deltaTime : 0.0f;
        newVelocity.x = isGrabbing ? newVelocity.x / 2f : newVelocity.x;

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

        currentVelocity = newVelocity; 
    }

    private void UpdateAnimatorVariables()
    {
        if (playerAnimatorController == null)
        {
            return;
        }

        playerAnimatorController.movementSpeed = canMove ? Mathf.Abs(horizontalAxis) : 0.0f;
        playerAnimatorController.signedMovementSpeed = isFacingRight ? horizontalAxis : -horizontalAxis;
        playerAnimatorController.clampedMovementSpeed = Mathf.Abs(horizontalAxisRaw);
        playerAnimatorController.animatorSpeed = currentPlayerData.animatorSpeedMultiplier;
        playerAnimatorController.isFacingLeft = !isFacingRight;
        playerAnimatorController.isOnGround = isOnGround;
        playerAnimatorController.canMove = canMove;
        playerAnimatorController.canJump = canJump;
        playerAnimatorController.canLand = canLand;
        playerAnimatorController.isJumping = isJumping;
    }
}
