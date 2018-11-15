using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] PlayerController PlayerController;

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
