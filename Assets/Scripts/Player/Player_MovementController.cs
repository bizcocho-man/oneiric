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
    private float inputJump;

    // Movement
    private Vector3 currentVelocity;
    private bool isFacingRight = true;
    private bool isJumping;
    [HideInInspector] public bool isGrabbing;

    private bool isOnGround;
    public bool IsOnGround
    {
        get
        {
            return isOnGround;
        }

        set
        {
            isOnGround = value;
            isJumping = false;
        }
    }

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
            isJumping = true;
        }
    }

    private void DetectDirection()
    {
        if (isGrabbing)
        {
            return;
        }

        if (horizontalAxis > 0.0f && !isFacingRight)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else if (horizontalAxis < 0.0f && isFacingRight)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }

    private void CalculateCurrentVelocity()
    {
        // Set CurrentVelocity based on input
        Vector3 newVelocity = rigidBody.velocity;

        newVelocity.z = IsOnGround ? horizontalAxis * currentPlayerData.movementSpeed * Time.deltaTime : horizontalAxis * currentPlayerData.movementSpeed * Time.deltaTime;
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
        playerAnimatorController.isJumping = isJumping;
    }
}
