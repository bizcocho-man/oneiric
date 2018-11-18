using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PressableObject : InteractableObject
{
    override public void StartInteracting()
    {
        if (!hasBeenInteracted)
        {
            EndInteracting();
        }
    }

    override public void EndInteracting()
    {
        if (!hasBeenInteracted)
        {
            hasBeenInteracted = true;
        }
    }
}
