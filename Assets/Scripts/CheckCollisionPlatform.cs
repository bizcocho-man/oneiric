using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionPlatform : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
