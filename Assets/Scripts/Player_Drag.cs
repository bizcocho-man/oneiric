using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Drag : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float raycastLength;
    [SerializeField] private Vector3 raycastPosition;

    private Rigidbody rigidBody;
    private GameObject objectBeingDragged;
    private FixedJoint objectFixedJoint;
    private RaycastHit hit;
    private bool didHit;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        //int layerMask = 1 << 11;

        didHit = Physics.Raycast(transform.position + raycastPosition, transform.forward, out hit, raycastLength, layerMask);

        if (didHit)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider != null && Input.GetKeyDown(KeyCode.E))
                {
                    objectBeingDragged = hit.collider.gameObject;

                    objectFixedJoint = objectBeingDragged.GetComponent<FixedJoint>();
                    if (objectFixedJoint != null)
                    {
                        objectFixedJoint.connectedBody = rigidBody;
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.E) && objectBeingDragged != null)
            {
                if (objectFixedJoint != null)
                {
                    objectFixedJoint.connectedBody = null;
                    objectFixedJoint = null;
                }

                Debug.Log("Sale");
            }
        }

        Debug.DrawRay(transform.position + raycastPosition, transform.forward * raycastLength, didHit ? Color.red : Color.green);
    }
}
