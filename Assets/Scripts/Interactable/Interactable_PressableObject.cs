﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PressableObject : InteractableObject
{
    public GameObject objectToActivate;
    public float timeToWaitBeforeStart;
    public Material interactedMaterial;

    private BehaviourObject behaviourObject;
    private MeshRenderer meshRenderer;

    public bool isReusable;

    private void Start()
    {
        if (objectToActivate != null)
        {
            behaviourObject = objectToActivate.GetComponent<BehaviourObject>();
        }

        meshRenderer = GetComponent<MeshRenderer>();
    }

    override public void StartInteracting()
    {
        StartCoroutine(StartInteractingDelayed());
        //if (!hasBeenInteracted)
        //{
        //    behaviourObject.ActivateBehaviour();
        //    EndInteracting();
        //}
    }

    override public void EndInteracting()
    {
        //if (!hasBeenInteracted)
        //{
        //    hasBeenInteracted = !isReusable;
        //    canBeInteracted = isReusable;
        //}
    }

    // Ugly solution, but no time to waste
    private IEnumerator StartInteractingDelayed()
    {
        yield return new WaitForSeconds(timeToWaitBeforeStart);

        if (!hasBeenInteracted)
        {
            yield return new WaitForSeconds(timeToWaitBeforeStart);
            behaviourObject.ActivateBehaviour();

            Material[] sharedMaterials = meshRenderer.sharedMaterials;
            sharedMaterials[0] = interactedMaterial;
            meshRenderer.sharedMaterials = sharedMaterials;

            hasBeenInteracted = !isReusable;
            canBeInteracted = isReusable;
        }
    }
}
