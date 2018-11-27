using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour
{
    [SerializeField] private GameObject cameraToDeactivate;
    [SerializeField] private GameObject cameraToActivate;

    private void OnTriggerEnter(Collider other)
    {
        cameraToDeactivate.SetActive(false);
        cameraToActivate.SetActive(true);
    }
}
