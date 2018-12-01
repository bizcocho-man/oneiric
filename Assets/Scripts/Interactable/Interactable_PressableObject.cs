using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PressableObject : InteractableObject
{
    public GameObject objectToActivate;
    public float timeToWaitBeforeStart = 0.25f;
    public Material interactedMaterial;

    private BehaviourObject behaviourObject;
    private MeshRenderer meshRenderer;

    public bool isReusable;

    private Material startMaterial;
    private bool firstUse = true;

    private void Start()
    {
        if (objectToActivate != null)
        {
            behaviourObject = objectToActivate.GetComponent<BehaviourObject>();
        }

        meshRenderer = GetComponent<MeshRenderer>();
        startMaterial = meshRenderer.sharedMaterials[0];
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
            behaviourObject.ActivateBehaviour();

            Material[] sharedMaterials = meshRenderer.sharedMaterials;

            if (firstUse)
            {
                firstUse = false;

                sharedMaterials[0] = interactedMaterial;
                meshRenderer.sharedMaterials = sharedMaterials;
            }
            else
            {
                firstUse = true;

                sharedMaterials[0] = startMaterial;
                meshRenderer.sharedMaterials = sharedMaterials;
            }

            hasBeenInteracted = !isReusable;
            canBeInteracted = isReusable;
        }
    }

    public override void ResetObject()
    {
        hasBeenInteracted = false;
        canBeInteracted = true;

        Material[] sharedMaterials = meshRenderer.sharedMaterials;
        sharedMaterials[0] = startMaterial;
        meshRenderer.sharedMaterials = sharedMaterials;
    }
}
