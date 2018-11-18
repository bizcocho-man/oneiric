using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FeetCollision : MonoBehaviour
{
    private Player_MovementController playerMovementController;

    private void Start()
    {
        playerMovementController = transform.parent.GetComponent<Player_MovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerMovementController.IsOnGround = true;
    }

    private void OnTriggerStay(Collider other)
    {
        playerMovementController.IsOnGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerMovementController.IsOnGround = false;
    }
}
