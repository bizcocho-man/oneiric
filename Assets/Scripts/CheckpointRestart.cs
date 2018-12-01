using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointRestart : MonoBehaviour
{
    public InteractableObject[] interactableObjectsToReset;
    public BehaviourObject[] behaviourObjectsToReset;
    public Behaviour_Enemy[] behaviourEnemiesToReset;

    public void RestartObjects()
    {
        for (int i = 0; i < interactableObjectsToReset.Length; ++i)
        {
            interactableObjectsToReset[i].ResetObject();
        }

        for (int i = 0; i < behaviourObjectsToReset.Length; ++i)
        {
            behaviourObjectsToReset[i].ResetObejct();
        }

        for (int i = 0; i < behaviourEnemiesToReset.Length; ++i)
        {
            behaviourEnemiesToReset[i].Restart();
        }
    }
}
