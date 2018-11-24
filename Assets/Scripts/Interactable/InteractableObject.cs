using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool hasBeenInteracted;

    virtual public void StartInteracting()
    {
        Debug.Log("StartInteracting");
    }

    virtual public void EndInteracting()
    {
        Debug.Log("EndInteracting");
    }
}
