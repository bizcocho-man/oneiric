using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    [SerializeField] private float movementSpeed = 100f;
    [SerializeField] private float jumpForce = 200f;

    // Components
    private Rigidbody rigidBody;
    public Vector3 CurrentVelocity { get; private set; }
    private bool isFacingRight = true;
    private bool isJumping;

    // Input
    private float horizontalAxis;
    private float inputJump;

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

    public float distance;
    GameObject box;
    public LayerMask boxMask;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        DetectJumping();
        DetectDirection();
        CalculateCurrentVelocity();

        Physics2D.queriesStartInColliders = false;
        int layerMask = 1 << 11;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, distance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider != null && Input.GetKeyDown(KeyCode.E))
                {
                    box = hit.collider.gameObject;
                    box.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
                }
            }
            else if (Input.GetKeyUp(KeyCode.E) && box)
            {
                box.GetComponent<FixedJoint>().connectedBody = null;
                Debug.Log("Sale");
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * distance, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = CurrentVelocity;
    }

    private void GetInput()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
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
        // Facing right/left detection
        if (horizontalAxis > 0.0f && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (horizontalAxis < 0.0f && isFacingRight)
        {
            isFacingRight = false;
        }
    }

    private void CalculateCurrentVelocity()
    {
        // Set CurrentVelocity based on input
        Vector3 newVelocity = rigidBody.velocity;

        newVelocity.z = IsOnGround ? horizontalAxis * movementSpeed * Time.deltaTime : horizontalAxis * movementSpeed * Time.deltaTime;
        newVelocity.y = isJumping ? jumpForce : newVelocity.y;

        CurrentVelocity = newVelocity;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
