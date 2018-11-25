using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_MovementSpeedMultiplier : StateMachineBehaviour
{
    [Range(0.0f, 1.0f)] public float animationPercentage;
    public float multiplierValue = 1.0f;
    public bool resetOnExit;

    private Player_MovementController playerMovementController;
    private bool hasBeenApplied;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (playerMovementController == null)
        {
            playerMovementController = animator.transform.GetComponent<Player_MovementController>();
        }
        hasBeenApplied = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float normalizedTime = animatorStateInfo.normalizedTime - Mathf.Floor(animatorStateInfo.normalizedTime);

        if (hasBeenApplied || normalizedTime < animationPercentage || playerMovementController == null)
        {
            return;
        }

        playerMovementController.movementSpeedMultiplier = multiplierValue;
        hasBeenApplied = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!resetOnExit || playerMovementController == null || !hasBeenApplied)
        {
            return;
        }

        playerMovementController.movementSpeedMultiplier = 1.0f;
    }
}
