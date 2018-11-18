using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player_MovementController : MonoBehaviour
{
    public float movementSpeed = 100f;
    public float jumpForce = 200f;

    // Components
    private Animator animator;
    private Rigidbody rigidBody;
    private Vector3 currentVelocity;
    private bool isFacingRight = true;
    private bool isJumping;
    private float animatorDampSpeed;

    // Input
    private float horizontalAxis;
    private float horizontalAxisClamped;
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

        horizontalAxisClamped = horizontalAxis != 0.0f ? horizontalAxis < 0.0f ? -1 : 1 : 0.0f;

        newVelocity.z = IsOnGround ? horizontalAxisClamped * movementSpeed * Time.deltaTime : horizontalAxisClamped * movementSpeed * Time.deltaTime;
        newVelocity.y = isJumping ? jumpForce : newVelocity.y;

        currentVelocity = newVelocity;

        animatorDampSpeed = horizontalAxis == 0 ? 0.05f : 0.15f;
        animator.SetFloat(speedHash, Mathf.Abs(horizontalAxis));
    }
}
