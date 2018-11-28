using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourObject_DoorTimer : BehaviourObject
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public MeshRenderer internLeftDoor;
    public MeshRenderer internRightDoor;
    public Material openMaterial;
    private Material closeMaterial;

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
        //boxCollider = GetComponent<BoxCollider>();
        closeMaterial = internLeftDoor.sharedMaterials[1];
    }

    override public void ActivateBehaviour()
    {
        currentTime = 0;

        if (!isActivating)
        {
            isActivating = true;
            //boxCollider.enabled = false;
            animationComponent.clip = OpenDoor;
            animationComponent.Play();

            Material[] sharedMaterials = internLeftDoor.sharedMaterials;
            sharedMaterials[1] = openMaterial;
            internLeftDoor.sharedMaterials = sharedMaterials;

            sharedMaterials = internRightDoor.sharedMaterials;
            sharedMaterials[1] = openMaterial;
            internRightDoor.sharedMaterials = sharedMaterials;
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
            //boxCollider.enabled = true;
            animationComponent.clip = CloseDoor;
            animationComponent.Play();

            Material[] sharedMaterials = internLeftDoor.sharedMaterials;
            sharedMaterials[1] = closeMaterial;
            internLeftDoor.sharedMaterials = sharedMaterials;

            sharedMaterials = internRightDoor.sharedMaterials;
            sharedMaterials[1] = closeMaterial;
            internRightDoor.sharedMaterials = sharedMaterials;
        }
    }
}
