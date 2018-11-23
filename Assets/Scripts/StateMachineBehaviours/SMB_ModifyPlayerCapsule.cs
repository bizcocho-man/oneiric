using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_ModifyPlayerCapsule : StateMachineBehaviour
{
    [Range(0.0f, 1.0f)]  public float animationPercentage;
    public SO_CapsuleColliderData newCapsuleData;
    
    public bool resetOnExit;
    public SO_CapsuleColliderData originalCapsuleData;

    private CapsuleCollider capsuleCollider;
    private bool hasBeenModified;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        capsuleCollider = animator.transform.GetComponent<CapsuleCollider>();
        hasBeenModified = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float normalizedTime = animatorStateInfo.normalizedTime - Mathf.Floor(animatorStateInfo.normalizedTime);

        if (hasBeenModified || normalizedTime < animationPercentage || capsuleCollider == null || newCapsuleData == null)
        {
            return;
        }

        capsuleCollider.center = newCapsuleData.center;
        capsuleCollider.radius = newCapsuleData.radius;
        capsuleCollider.height = newCapsuleData.height;
        hasBeenModified = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!resetOnExit || capsuleCollider == null || originalCapsuleData == null)
        {
            return;
        }

        capsuleCollider.center = originalCapsuleData.center;
        capsuleCollider.radius = originalCapsuleData.radius;
        capsuleCollider.height = originalCapsuleData.height;
    }
}
