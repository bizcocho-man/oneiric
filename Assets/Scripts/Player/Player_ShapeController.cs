using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_MovementController))]
[RequireComponent(typeof(Player_Interactable))]
public class Player_ShapeController : MonoBehaviour
{
    [HideInInspector] public bool normalShape;

    [SerializeField] private SO_PlayerData normalData;
    [SerializeField] private SO_PlayerData oniricData;

    private Player_MovementController playerMovementController;
    private Player_Interactable playerInteractable;

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
