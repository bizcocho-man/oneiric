﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FeetCollision : MonoBehaviour
{
    [SerializeField] private LayerMask layers;
    [SerializeField] private float distance;

    private Player_MovementController playerMovementController;
    private bool hit;

    private void Start()
    {
        playerMovementController = transform.parent.GetComponent<Player_MovementController>();
    }

    private void Update()
    {
        hit = Physics.Raycast(transform.position, Vector3.down, distance, layers);
        playerMovementController.isOnGround = hit;

        Debug.DrawRay(transform.position, Vector3.down * distance, hit ? Color.red : Color.green);
    }
}
