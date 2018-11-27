using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager _instance;
    public static CheckpointManager Instance { get { return _instance; } }

    private GameObject currentCheckpoint;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }

    public void SetCurrentCheckpoint(GameObject newCheckpoint)
    {
        if (newCheckpoint != null)
        {
            currentCheckpoint = newCheckpoint;
        }
    }
}
