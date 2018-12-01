using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool hasBeenInteracted;
    public bool canBeInteracted = true;

    virtual public void StartInteracting()
    {
        Debug.Log("StartInteracting");
    }

    virtual public void EndInteracting()
    {
        Debug.Log("EndInteracting");
    }

    virtual public void ResetObject()
    {

    }
}
