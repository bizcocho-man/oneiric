using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionEndGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        UI_Manager.Instance.EndGame();
    }
}
