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

    private void Start()
    {
        anim = GetComponent<Animation>();
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
}
