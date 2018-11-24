using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget_FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 localOffset;

    private Vector3 globalOffset;

    private void Start()
    {
        globalOffset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = target.position + globalOffset;
    }
}
