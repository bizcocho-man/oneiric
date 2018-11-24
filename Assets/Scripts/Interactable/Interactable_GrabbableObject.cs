using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable_GrabbableObject : InteractableObject
{
    private Rigidbody rigibodyPlayer;
    private Rigidbody rigidbodyGO;
    private FixedJoint fixedJoint;

    private Player_MovementController playerMovementController;

    private bool isFalling;

    private LayerMask layerGround;
    private RaycastHit hit;

    private void Start()
    {
        isFalling = true;
        rigidbodyGO = GetComponent<Rigidbody>();
        rigibodyPlayer = FindObjectOfType<Player_MovementController>().GetComponent<Rigidbody>();
        playerMovementController = FindObjectOfType<Player_MovementController>();
        layerGround = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y * 3f, layerGround))
        {
            if (rigidbodyGO.velocity.y == 0f && isFalling)
            {
                isFalling = false;
                fixedJoint = gameObject.AddComponent<FixedJoint>();

                rigidbodyGO.mass = 1;
            }
        }
        else
        {
            if (!isFalling)
            {
                isFalling = true;
                Destroy(fixedJoint);
                EndInteracting();

                rigidbodyGO.mass = 100;
            }
        }
    }

    override public void StartInteracting()
    {
        if (!hasBeenInteracted)
        {
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
            fixedJoint.connectedBody = rigibodyPlayer;
        }
    }

    override public void EndInteracting()
    {
        hasBeenInteracted = false;
        playerMovementController.isGrabbing = false;

        if (fixedJoint)
        {
            fixedJoint.connectedBody = null;
        }

        rigidbodyGO.constraints = RigidbodyConstraints.FreezeRotationX
                          | RigidbodyConstraints.FreezeRotationY;

        rigibodyPlayer.constraints = RigidbodyConstraints.FreezeRotationX
                           | RigidbodyConstraints.FreezeRotationZ
                           | RigidbodyConstraints.FreezePositionZ;
    }
}
