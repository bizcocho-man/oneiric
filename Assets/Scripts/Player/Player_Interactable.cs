using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Interactable : MonoBehaviour
{
    [HideInInspector] public bool canInteract;
    [HideInInspector] public GameObject interactableObject;

    private Player_MovementController playerMovementController;

    private void Start()
    {
        playerMovementController = GetComponent<Player_MovementController>();
    }

    private void Update()
    {
        if (!canInteract)
        {
            return;
        }

        if (interactableObject != null && playerMovementController.isOnGround)
        {
            if (interactableObject.GetComponent<InteractableObject>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactableObject.GetComponent<InteractableObject>().StartInteracting();
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    interactableObject.GetComponent<InteractableObject>().EndInteracting();
                }
            }
        }
    }
}
