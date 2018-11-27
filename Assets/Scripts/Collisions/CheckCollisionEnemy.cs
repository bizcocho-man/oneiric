using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCollisionEnemy : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = Object.FindObjectOfType<Player_MovementController>().gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.transform.position = CheckpointManager.Instance.GetCurrentCheckpoint().transform.position;
    }
}
