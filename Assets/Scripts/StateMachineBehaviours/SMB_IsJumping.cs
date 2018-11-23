using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_IsJumping : StateMachineBehaviour
{
    [Range(0.0f, 1.0f)] public float animationPercentage;
    public bool isJumping;
    public bool resetOnExit;

    private Player_MovementController playerMovementController;
    private bool hasBeenApplied;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerMovementController = animator.transform.GetComponent<Player_MovementController>();
        hasBeenApplied = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float normalizedTime = animatorStateInfo.normalizedTime - Mathf.Floor(animatorStateInfo.normalizedTime);

        if (hasBeenApplied || normalizedTime < animationPercentage || playerMovementController == null)
        {
            return;
        }

        playerMovementController.isJumping = isJumping;
        hasBeenApplied = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!resetOnExit || playerMovementController == null || !hasBeenApplied)
        {
            return;
        }

        playerMovementController.canMove = !isJumping;
    }
}
