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

    private bool isButtonReleased = true;

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

        if (UI_Manager.Instance.isPausedGame)
        {
            return;
        }

        if (playerMovementController.isOnGround)
        {
            if (Input.GetButtonDown("Fire1") && interactableObjectComponent.canBeInteracted && isButtonReleased)
            {
                isButtonReleased = false;
                interactableObjectComponent.StartInteracting();
                UpdateAnimatorVariables(true);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                isButtonReleased = true;
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
                if (value && !playerAnimatorController.isInteractingPressable)
                {
                    playerAnimatorController.isInteractingPressable = value;
                }

                if (!value)
                {
                    playerAnimatorController.isInteractingPressable = value;
                }
                
                break;
            //case tag_InteractableGrabbable:
            //    playerAnimatorController.isGrabbing = value;
            //    break;
        }
    }
}
