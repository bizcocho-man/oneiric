using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourObject_MovablePlatformClosed : BehaviourObject
{
    public float speed;
    public float dstY;
    public float dstX;

    private Vector3 dest;
    private Vector3 start;
    private bool isActivated;
    private float fraction = 0f;
    public bool doOnce;
    private bool doOnceInProgress;

    public GameObject doorPlatform;
    public float moveDoor;

    private Vector3 destDoor;
    private Vector3 startDoor;

    private bool isOpenDoor;

    private void Start()
    {
        start = transform.position;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z);

        startDoor = doorPlatform.transform.localPosition;
        destDoor = new Vector3(doorPlatform.transform.localPosition.x, doorPlatform.transform.localPosition.y + moveDoor);
        doorPlatform.transform.localPosition = destDoor;
    }

    override public void ActivateBehaviour()
    {
        isActivated = true;
        isOpenDoor = !isOpenDoor;

        if (doOnce)
        {
            doOnceInProgress = true;
        }
    }

    public void DeactivateBehaviour()
    {
        isActivated = false;
    }

    private void Update()
    {
        if (isActivated && doOnceInProgress)
        {
            if (fraction < 1)
            {
                fraction += Time.fixedDeltaTime * speed;
                transform.position = Vector3.Lerp(start, dest, fraction);

                doorPlatform.transform.localPosition = Vector3.Lerp(destDoor, startDoor, fraction);
            }
            else
            {
                Vector3 aux = start;
                start = dest;
                dest = aux;

                aux = startDoor;
                startDoor = destDoor;
                destDoor = aux;

                fraction = 0;

                if (doOnce)
                {
                    doOnceInProgress = false;
                }
            }
        } 
    }
}
