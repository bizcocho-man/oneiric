using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallCollision : MonoBehaviour
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
        hit = Physics.Raycast(transform.position, transform.forward, distance, layers);
        playerMovementController.canMove = !hit;

        Debug.DrawRay(transform.position, transform.forward * distance, hit ? Color.red : Color.green);
    }
}
