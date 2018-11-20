using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourObject_Door : BehaviourObject
{
    public GameObject LeftDoor;
    public GameObject RightDoor;

    private BoxCollider boxCollider;
    private Animation anim;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animation>();
    }

    override public void ActivateBehaviour()
    {
        boxCollider.enabled = false;
        anim.Play();
    }
}
