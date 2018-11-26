using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourObject_MovablePlatform : BehaviourObject
{
    public float speed;
    public float dstY;
    public float dstX;
    public float dstZ;

    private Vector3 dest;
    private Vector3 start;
    private bool isActivated;
    private float fraction = 0f;
    public bool doOnce;
    private bool doOnceInProgress;

    private void Start()
    {
        start = transform.position;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z + dstZ);
    }

    override public void ActivateBehaviour()
    {
        isActivated = true;

        if (doOnce)
        {
            doOnceInProgress = true;
        }
    }

    public void DeactivateBehaviour()
    {
        isActivated = false;
    }

    private void FixedUpdate()
    {
        if (isActivated || doOnceInProgress)
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

                if (doOnce)
                {
                    doOnceInProgress = false;
                }
            }
        } 
    }
}
