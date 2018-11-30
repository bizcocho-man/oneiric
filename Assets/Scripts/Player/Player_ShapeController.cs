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
    [SerializeField] private SkinnedMeshRenderer characterRenderer;
    [SerializeField] private SkinnedMeshRenderer hairRenderer;

    private Player_MovementController playerMovementController;
    private Player_Interactable playerInteractable;

    private Material[] newMaterials;

    private void Start()
    {
        playerMovementController = GetComponent<Player_MovementController>();
        playerInteractable = GetComponent<Player_Interactable>();

        normalShape = true;

        SetShapeVariables();
        SetShapeMaterials();
    }
	
	private void Update ()
    {
        if (UI_Manager.Instance.isPausedGame)
        {
            return;
        }

        if (Input.GetButtonDown("Fire2") && playerMovementController.isOnGround)
        {
            normalShape = !normalShape;

            SetShapeVariables();
            SetShapeMaterials();
        }
    }

    private void SetShapeVariables()
    {
        playerMovementController.currentPlayerData = normalShape ? normalData : oniricData;
        playerInteractable.canInteract = normalShape;
    }

    private void SetShapeMaterials()
    {
        // Hair
        newMaterials = hairRenderer.sharedMaterials;
        newMaterials[0] = normalShape ? normalData.hairMaterial : oniricData.hairMaterial;
        hairRenderer.sharedMaterials = newMaterials;

        // Body
        newMaterials = characterRenderer.sharedMaterials;
        newMaterials[0] = normalShape ? normalData.shirtMaterial : oniricData.shirtMaterial;
        newMaterials[1] = normalShape ? normalData.pantsMaterial : oniricData.pantsMaterial;
        newMaterials[2] = normalShape ? normalData.skinMaterial : oniricData.skinMaterial;
        characterRenderer.sharedMaterials = newMaterials;
    }
}
