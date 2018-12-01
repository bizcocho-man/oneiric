using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointRestart checkpointRestart;

    private void Start()
    {
        checkpointRestart = GetComponent<CheckpointRestart>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager.Instance.SetCurrentCheckpoint(this.gameObject);
    }

    public void RestartLevel()
    {
        checkpointRestart.RestartObjects();
    }
}
