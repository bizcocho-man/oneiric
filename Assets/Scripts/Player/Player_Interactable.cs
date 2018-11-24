using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Interactable : MonoBehaviour
{
    [HideInInspector] public bool canInteract;
    [HideInInspector] public GameObject interactableObject;

    private Player_MovementController playerMovementController;
    private Player_AnimatorController playerAnimatorController;

    private const string tag_InteractablePressable = "Interactable_Pressable";
    private const string tag_InteractableGrabbable = "Interactable_Grabbable";

    private void Start()
    {
        playerMovementController = GetComponent<Player_MovementController>();
        playerAnimatorController = GetComponent<Player_AnimatorController>();
    }

    private void Update()
    {
        if (!canInteract || interactableObject == null)
        {
            return;
        }

        InteractableObject interactableObjectComponent = interactableObject.GetComponent<InteractableObject>();
        if (interactableObjectComponent == null)
        {
            return;
        }

        if (playerMovementController.isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.E) && interactableObjectComponent.canBeInteracted)
            {
                interactableObjectComponent.StartInteracting();
                UpdateAnimatorVariables(true);
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                interactableObjectComponent.EndInteracting();
                UpdateAnimatorVariables(false);
            }
        }
        else if (interactableObjectComponent.hasBeenInteracted)
        {
            interactableObjectComponent.EndInteracting();
            UpdateAnimatorVariables(false);
        }
    }

    private void UpdateAnimatorVariables(bool value)
    {
        switch (interactableObject.tag)
        {
            case tag_InteractablePressable:
                playerAnimatorController.isInteractingPressable = value;
                break;
            case tag_InteractableGrabbable:
                playerAnimatorController.isInteractingGrabbable = value;
                break;
        }
    }
}
