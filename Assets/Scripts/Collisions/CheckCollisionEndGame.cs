using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionEndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UI_Manager.Instance.isPausedGame = true;
        UI_Manager.Instance.EndGame();
    }
}
