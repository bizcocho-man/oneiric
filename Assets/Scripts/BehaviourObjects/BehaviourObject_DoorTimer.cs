using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourObject_DoorTimer : BehaviourObject
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public float time;
    private float currentTime;

    private bool isActivating;

    private Animation animationComponent;
    public AnimationClip OpenDoor;
    public AnimationClip CloseDoor;

    private BoxCollider boxCollider;

    private void Start()
    {
        animationComponent = GetComponent<Animation>();
        boxCollider = GetComponent<BoxCollider>();

    }

    override public void ActivateBehaviour()
    {
        currentTime = 0;

        if (!isActivating)
        {
            isActivating = true;
            boxCollider.enabled = false;
            animationComponent.clip = OpenDoor;
            animationComponent.Play();
        }  
    }

    private void Update()
    {
        if (isActivating)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= time)
            {
                DeactivateBehaviour();
            }
        }
    }

    public void DeactivateBehaviour()
    {
        if (isActivating)
        {
            isActivating = false;
            boxCollider.enabled = true;
            animationComponent.clip = CloseDoor;
            animationComponent.Play();
        }
    }
}
