using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Feetcollision : MonoBehaviour
{
    [SerializeField] Player_MovementController PlayerController;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController.IsOnGround = true;
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerController.IsOnGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController.IsOnGround = false;
    }
}
