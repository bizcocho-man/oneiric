using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_GrabbableObject : InteractableObject
{
    private Rigidbody rigibodyPlayer;
    private Rigidbody rigidbodyGO;

    private Player_MovementController playerMovementController;

    private bool isFalling;

    private LayerMask layerGround;
    private RaycastHit hit;

    private void Start()
    {
        isFalling = true;
        CreateRigidbody();
        rigibodyPlayer = FindObjectOfType<Player_MovementController>().GetComponent<Rigidbody>();
        playerMovementController = FindObjectOfType<Player_MovementController>();
        layerGround = LayerMask.GetMask("Ground");

        rigidbodyGO.constraints = RigidbodyConstraints.FreezeRotationX
                                | RigidbodyConstraints.FreezeRotationZ
                                | RigidbodyConstraints.FreezePositionZ;
    }

    private void CreateRigidbody()
    {
        if (!rigidbodyGO)
        {
            rigidbodyGO = gameObject.AddComponent<Rigidbody>();
            rigidbodyGO.mass = 100;
        }  
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y * 4f, layerGround))
        {
            if (!hasBeenInteracted)
            {
                if (rigidbodyGO.velocity.y == 0f && isFalling)
                {
                    isFalling = false;
                }
            }     
        }
        else
        {
            if (!isFalling)
            {
                isFalling = true;
                EndInteracting();
            }
        }
    }

    override public void StartInteracting()
    {
        if (!hasBeenInteracted)
        {
            Destroy(rigidbodyGO);

            rigidbodyGO.constraints = RigidbodyConstraints.FreezeRotationX
                                    | RigidbodyConstraints.FreezeRotationY
                                    | RigidbodyConstraints.FreezeRotationZ
                                    | RigidbodyConstraints.FreezePositionY
                                    | RigidbodyConstraints.FreezePositionZ;

            rigibodyPlayer.constraints = RigidbodyConstraints.FreezeRotationX
                                       | RigidbodyConstraints.FreezeRotationY
                                       | RigidbodyConstraints.FreezeRotationZ
                                       | RigidbodyConstraints.FreezePositionY
                                       | RigidbodyConstraints.FreezePositionZ;

            hasBeenInteracted = true;
            playerMovementController.isGrabbing = true;

            transform.parent = playerMovementController.transform;
        }
    }

    override public void EndInteracting()
    {
        CreateRigidbody();
        


        hasBeenInteracted = false;
        playerMovementController.isGrabbing = false;

        transform.parent = null;

        rigibodyPlayer.constraints = RigidbodyConstraints.FreezeRotationX
                                   | RigidbodyConstraints.FreezeRotationZ
                                   | RigidbodyConstraints.FreezePositionZ;

        rigidbodyGO.constraints = RigidbodyConstraints.FreezeRotationX
                                | RigidbodyConstraints.FreezeRotationY
                                | RigidbodyConstraints.FreezePositionZ;
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = Vector3.down * transform.localScale.y * 4f;
        Gizmos.DrawRay(transform.position, direction);
    }
}
