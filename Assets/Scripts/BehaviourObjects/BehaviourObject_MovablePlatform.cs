using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourObject_MovablePlatform : BehaviourObject
{
    public float speed;
    public float dstY;
    public float dstX;

    private Vector3 dest;
    private Vector3 start;
    private bool bIsActivated;
    private float fraction = 0f;

    private void Start()
    {
        start = transform.position;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z);
    }

    override public void ActivateBehaviour()
    {
        bIsActivated = true;
    }

    public void DeactivateBehaviour()
    {
        bIsActivated = false;
    }

    private void FixedUpdate()
    {
        if (bIsActivated)
        {
            if (fraction < 1)
            {
                fraction += Time.fixedDeltaTime * speed;
                transform.position = Vector3.Lerp(start, dest, fraction);
            }
            else
            {
                Vector3 aux = start;
                start = dest;
                dest = aux;

                fraction = 0;
            }
        } 
    }
}
