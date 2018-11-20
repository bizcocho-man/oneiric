using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeShape : MonoBehaviour
{
    [SerializeField] private SO_PlayerData normalData;
    [SerializeField] private SO_PlayerData oniricData;

    private Player_MovementController playerMovementController;
    private Player_Interactable playerInteractable;

    private bool normalShape;

    private void Start()
    {
        playerMovementController = GetComponent<Player_MovementController>();
        playerInteractable = GetComponent<Player_Interactable>();

        normalShape = true;

        SetShapeVariables();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            normalShape = !normalShape;

            SetShapeVariables();
        }
    }

    void SetShapeVariables()
    {
        playerMovementController.currentPlayerData = normalShape ? normalData : oniricData;
        playerInteractable.canInteract = normalShape;
    }
}
