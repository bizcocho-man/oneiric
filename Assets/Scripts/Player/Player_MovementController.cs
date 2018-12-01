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
    private int horizontalAxisClamped;
    private bool inputJump;

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
    [HideInInspector] public bool isPressing;
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
        if (!UI_Manager.Instance.isPausedGame && UI_Manager.Instance.canReceiveInput)
        {
            GetInput();
            DetectJumping();
        }

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
        if (Input.GetButtonDown("RestartLevel") && CheckpointManager.Instance.GetCurrentCheckpoint() != null)
        {
            UI_Manager.Instance.FadeInDeath();
        }

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        horizontalAxisClamped = horizontalAxis > 0.5 ? 1 : horizontalAxis < -0.5 ? -1 : 0;
        inputJump = Input.GetButton("Jump");

        canMove = !isMovementBlocked && !isAgainstObstacle;
    }

    private void DetectJumping()
    {
        if (inputJump && !isJumping && isOnGround)
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

        newVelocity.x = canMove ? horizontalAxisClamped * currentPlayerData.movementSpeed * movementSpeedMultiplier * Time.deltaTime : 0.0f;
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

        playerAnimatorController.movementSpeed = canMove ? Mathf.Abs(horizontalAxisClamped) : 0.0f;
        playerAnimatorController.signedMovementSpeed = isFacingRight ? horizontalAxisClamped : -horizontalAxisClamped;
        playerAnimatorController.clampedMovementSpeed = Mathf.Abs(horizontalAxisClamped);
        playerAnimatorController.animatorSpeed = currentPlayerData.animatorSpeedMultiplier;
        playerAnimatorController.isFacingLeft = !isFacingRight;
        playerAnimatorController.isOnGround = isOnGround;
        playerAnimatorController.canMove = canMove;
        playerAnimatorController.canJump = canJump;
        playerAnimatorController.canLand = canLand;
        playerAnimatorController.isJumping = isJumping;
        playerAnimatorController.isGrabbing = isGrabbing;
    }
}
