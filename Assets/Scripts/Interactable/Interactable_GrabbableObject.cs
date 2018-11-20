using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
[RequireComponent(typeof(Rigidbody))]
public class Interactable_GrabbableObject : InteractableObject
{
    private Rigidbody rigibodyPlayer;
    private FixedJoint fixedJoint;

    private Player_MovementController playerMovementController;

    private void Start()
    {
        fixedJoint = GetComponent<FixedJoint>();
        rigibodyPlayer = FindObjectOfType<Player_MovementController>().GetComponent<Rigidbody>();
        playerMovementController = FindObjectOfType<Player_MovementController>();

        rigibodyPlayer.constraints = RigidbodyConstraints.FreezeRotationX
                                   | RigidbodyConstraints.FreezeRotationY
                                   | RigidbodyConstraints.FreezeRotationZ
                                   | RigidbodyConstraints.FreezePositionX;
    }

    override public void StartInteracting()
    {
        if (!hasBeenInteracted)
        {
            hasBeenInteracted = true;
            playerMovementController.isGrabbing = true;
            fixedJoint.connectedBody = rigibodyPlayer;
        }
    }

    override public void EndInteracting()
    {
        hasBeenInteracted = false;
        playerMovementController.isGrabbing = false;
        GetComponent<FixedJoint>().connectedBody = null;
    }
}
