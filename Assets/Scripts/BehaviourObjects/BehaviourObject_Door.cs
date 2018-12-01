using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourObject_Door : BehaviourObject
{
    public GameObject LeftDoor;
    public GameObject RightDoor;

    public MeshRenderer internLeftDoor;
    public MeshRenderer internRightDoor;

    public Material openMaterial;

    private BoxCollider boxCollider;
    private Animation anim;

    private Material startMaterial;
    private Vector3 startLeftDoorPosition;
    public Vector3 startRightDootPosition;
     
    private void Start()
    {
        anim = GetComponent<Animation>();

        startMaterial = internLeftDoor.sharedMaterials[1];
        startLeftDoorPosition = internLeftDoor.transform.position;
        startRightDootPosition = internRightDoor.transform.position;
    }

    // Ugly solution, but no time to waste
    override public void ActivateBehaviour()
    {
        anim.Play();

        Material[] sharedMaterials = internLeftDoor.sharedMaterials;
        sharedMaterials[1] = openMaterial;
        internLeftDoor.sharedMaterials = sharedMaterials;

        sharedMaterials = internRightDoor.sharedMaterials;
        sharedMaterials[1] = openMaterial;
        internRightDoor.sharedMaterials = sharedMaterials;
    }

    public override void ResetObejct()
    {
        Material[] sharedMaterials = internLeftDoor.sharedMaterials;
        sharedMaterials[1] = startMaterial;
        internLeftDoor.sharedMaterials = sharedMaterials;

        sharedMaterials = internRightDoor.sharedMaterials;
        sharedMaterials[1] = startMaterial;
        internRightDoor.sharedMaterials = sharedMaterials;

        internLeftDoor.transform.position = startLeftDoorPosition;
        internRightDoor.transform.position = startRightDootPosition;
    }
}
