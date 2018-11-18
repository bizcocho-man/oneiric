using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Interactable : MonoBehaviour
{
    [HideInInspector] public GameObject interactableObject;

    private Player_MovementController playerMovementController;

    private void Start()
    {
        playerMovementController = FindObjectOfType<Player_MovementController>();
    }

    private void Update()
    {
        if (interactableObject != null && playerMovementController.IsOnGround)
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
