using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player_MovementController : MonoBehaviour
{
    public SO_PlayerData normalPlayerData;
    public SO_PlayerData oniricPlayerData;
    private SO_PlayerData currentPlayerData;

    // Components
    private Animator animator;
    private Rigidbody rigidBody;
    private Vector3 currentVelocity;
    private bool isFacingRight = true;
    private bool isJumping;
    [HideInInspector] public bool isGrabbing;

    // Input
    private float horizontalAxis;
    private float inputJump;
    private int speedHash = Animator.StringToHash("Speed");

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
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        currentPlayerData = normalPlayerData;
    }

    void Update()
    {
        GetInput();
        DetectJumping();
        DetectDirection();
        CalculateCurrentVelocity();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = currentVelocity;
    }

    private void GetInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        inputJump = Input.GetAxis("Vertical");
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
        // Facing right/left detection

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

        // Jumping
        if (!isGrabbing)
        {
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
        animator.SetFloat(speedHash, Mathf.Abs(horizontalAxis));
    }
}
