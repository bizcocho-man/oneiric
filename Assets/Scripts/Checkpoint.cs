using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int priority;

    private CheckpointRestart checkpointRestart;

    private void Start()
    {
        checkpointRestart = GetComponent<CheckpointRestart>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (priority > CheckpointManager.Instance.GetCurrentCheckpoint().priority)
        {
            CheckpointManager.Instance.SetCurrentCheckpoint(this);
        }
    }

    public void RestartLevel()
    {
        if (checkpointRestart != null)
        {
            checkpointRestart.RestartObjects();
        }
    }
}
