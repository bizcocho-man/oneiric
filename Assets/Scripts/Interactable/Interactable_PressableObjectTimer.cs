using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PressableObjectTimer : InteractableObject
{
    public GameObject objectToActivate;
    public float timeToWaitBeforeStart = 0.25f;
    public float timer;
    public Material interactedMaterial;

    private BehaviourObject behaviourObject;
    private MeshRenderer meshRenderer;
    private Material originalMaterial;

    private void Start()
    {
        if (objectToActivate != null)
        {
            behaviourObject = objectToActivate.GetComponent<BehaviourObject_DoorTimer>();
        }

        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.sharedMaterials[0];
    }

    override public void StartInteracting()
    {
        //behaviourObject.ActivateBehaviour();
        StopCoroutine(StartInteractingDelayed());
        StartCoroutine(StartInteractingDelayed());
    }

    public override void EndInteracting()
    {
    }

    private IEnumerator StartInteractingDelayed()
    {
        yield return new WaitForSeconds(timeToWaitBeforeStart);
        behaviourObject.ActivateBehaviour();
        Material[] sharedMaterials = meshRenderer.sharedMaterials;
        sharedMaterials[0] = interactedMaterial;
        meshRenderer.sharedMaterials = sharedMaterials;

        yield return new WaitForSeconds(timer);
        sharedMaterials = meshRenderer.sharedMaterials;
        sharedMaterials[0] = originalMaterial;
        meshRenderer.sharedMaterials = sharedMaterials;
    }
}
