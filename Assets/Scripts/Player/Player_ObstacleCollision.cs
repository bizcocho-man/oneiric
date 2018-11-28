using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ObstacleCollision : MonoBehaviour
{
    [SerializeField] private LayerMask layers;
    public float distance = 2.3f;

    private Player_MovementController playerMovementController;
    private bool hit;

    private void Start()
    {
        playerMovementController = transform.parent.GetComponent<Player_MovementController>();
    }

    private void Update()
    {
        hit = Physics.Raycast(transform.position, transform.forward, distance, layers);
        playerMovementController.isAgainstObstacle = hit && playerMovementController.isOnGround;

        Debug.DrawRay(transform.position, transform.forward * distance, hit ? Color.red : Color.green);
    }
}
