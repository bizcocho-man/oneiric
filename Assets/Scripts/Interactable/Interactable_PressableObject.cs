using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PressableObject : InteractableObject
{
    public GameObject objectToActivate;

    private BehaviourObject behaviourObject;

    public bool isReusable;

    private void Start()
    {
        behaviourObject = objectToActivate.GetComponent<BehaviourObject>();
    }

    override public void StartInteracting()
    {
        if (!hasBeenInteracted)
        {
            behaviourObject.ActivateBehaviour();
            EndInteracting();
        }
    }

    override public void EndInteracting()
    {
        if (!hasBeenInteracted)
        {
            hasBeenInteracted = !isReusable;
            canBeInteracted = isReusable;
        }
    }
}
