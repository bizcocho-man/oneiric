using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_ModifyObstacleDetectionDistance : StateMachineBehaviour
{
    [Range(0.0f, 1.0f)] public float animationPercentage;
    public float distance;
    public bool resetOnExit;

    private Player_ObstacleCollision playerObstacleDetection;
    private float originalDistance;
    private bool hasBeenApplied;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (playerObstacleDetection == null)
        {
            playerObstacleDetection = animator.transform.GetComponentInChildren<Player_ObstacleCollision>();
        }

        hasBeenApplied = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float normalizedTime = animatorStateInfo.normalizedTime - Mathf.Floor(animatorStateInfo.normalizedTime);

        if (hasBeenApplied || normalizedTime < animationPercentage || playerObstacleDetection == null)
        {
            return;
        }

        originalDistance = playerObstacleDetection.distance;
        playerObstacleDetection.distance = distance;
        hasBeenApplied = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!resetOnExit || playerObstacleDetection == null || !hasBeenApplied)
        {
            return;
        }

        playerObstacleDetection.distance = originalDistance;
    }
}
