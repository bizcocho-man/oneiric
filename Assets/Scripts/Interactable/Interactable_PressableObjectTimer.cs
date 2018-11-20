using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PressableObjectTimer : InteractableObject
{
    public GameObject objectToActivate;

    private BehaviourObject behaviourObject;

    private void Start()
    {
        behaviourObject = objectToActivate.GetComponent<BehaviourObject_DoorTimer>();
    }

    override public void StartInteracting()
    {
        behaviourObject.ActivateBehaviour();
    }

    public override void EndInteracting()
    {
    }
}
