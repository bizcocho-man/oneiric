using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("MUERTE DESTRUCCIÓN");
    }
}
