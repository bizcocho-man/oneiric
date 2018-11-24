using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionPlatform : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Interactable_GrabbableObject>())
        {
            if (other.GetComponent<Interactable_GrabbableObject>().hasBeenInteracted)
            {
                return;
            }   
        }

        other.transform.parent = transform.parent;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
