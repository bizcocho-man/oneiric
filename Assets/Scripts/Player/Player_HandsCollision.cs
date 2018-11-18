using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HandsCollision : MonoBehaviour
{

    private Player_Interactable playerInteractable;

    private void Start()
    {
        playerInteractable = transform.parent.GetComponent<Player_Interactable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInteractable.interactableObject = other.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        playerInteractable.interactableObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInteractable.interactableObject = null;
    }
}
