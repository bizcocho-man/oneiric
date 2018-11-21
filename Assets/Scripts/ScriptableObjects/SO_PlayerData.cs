using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class SO_PlayerData : ScriptableObject
{
    // Movement
    public float movementSpeed;
    public float jumpSpeed;
    public float maxFallingSpeed;
    public float fallingAcceleraion;
    public float animatorSpeedMultiplier;

    // Materials
    public Material hairMaterial;
    public Material skinMaterial;
    public Material shirtMaterial;
    public Material pantsMaterial;
}
