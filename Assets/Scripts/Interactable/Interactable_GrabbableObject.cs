﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_GrabbableObject : InteractableObject
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float raycastLength = 4f;

    private Rigidbody rigibodyPlayer;
    private Rigidbody rigidbodyGO;

    private Player_MovementController playerMovementController;

    private bool isFalling;    
    private bool hit;

    private void Start()
    {
        isFalling = true;
        CreateRigidbody();
        rigibodyPlayer = FindObjectOfType<Player_MovementController>().GetComponent<Rigidbody>();
        playerMovementController = FindObjectOfType<Player_MovementController>();

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
        hit = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y * raycastLength, collisionLayer);
        Debug.DrawRay(transform.position, Vector3.down * transform.localScale.y * raycastLength, hit ? Color.green : Color.red);

        if (hit)
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
}
