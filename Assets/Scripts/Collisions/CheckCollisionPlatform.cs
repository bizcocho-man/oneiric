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

        if(other.gameObject.tag == "Player")
        {
            other.transform.parent.parent = transform.parent;
        }
        else if (other.gameObject.tag == "Interactable_Grabbable")
        {
            if (other.transform.parent.parent)
            {
                other.transform.parent = transform.parent;
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.transform.parent.parent)
            {
                other.transform.parent.parent = null;
            }      
        }
        else if (other.gameObject.tag == "Interactable_Grabbable")
        {
            // I don't understand
            //other.transform.parent = null;
        }      
    }
}
