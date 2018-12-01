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

    private Vector3 startPosition;

    private void Start()
    {
        start = transform.position;
        startPosition = transform.position;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z + dstZ);
    }

    override public void ActivateBehaviour()
    {
        isActivated = true;
    }

    public void DeactivateBehaviour()
    {
        isActivated = false;
    }

    private void Update()
    {
        if (isActivated)
        {
            if (fraction < 1)
            {
                fraction += Time.deltaTime * speed;
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
                    DeactivateBehaviour();
                }
            }
        } 
    }

    public override void ResetObejct()
    {
        fraction = 0f;
        isActivated = false;
        transform.position = startPosition;
        start = startPosition;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z + dstZ);
    }
}
