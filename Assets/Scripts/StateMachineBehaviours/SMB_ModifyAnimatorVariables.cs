using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_ModifyAnimatorVariables : StateMachineBehaviour
{
    [System.Serializable]
    public struct VariableData
    {
        [Range(0.0f, 1.0f)] public float animationPercentage;
        [HideInInspector] public bool hasBeenApplied;

        public enum VariableType { Float, Int, Bool, Trigger }
        public VariableType variableType;

        public string variableName;

        public float floatValue;
        public int intValue;
        public bool boolValue;
        public bool triggerValue;

        public void Apply(Animator animator, float normalizedTime)
        {
            if (!hasBeenApplied && normalizedTime >= animationPercentage)
            {
                switch (variableType)
                {
                    case VariableType.Float:
                        animator.SetFloat(variableName, floatValue);
                        hasBeenApplied = true;
                        break;
                    case VariableType.Int:
                        animator.SetInteger(variableName, intValue);
                        hasBeenApplied = true;
                        break;
                    case VariableType.Bool:
                        animator.SetBool(variableName, boolValue);
                        hasBeenApplied = true;
                        break;
                    case VariableType.Trigger:
                        if (triggerValue)
                        {
                            animator.SetTrigger(variableName);
                            hasBeenApplied = true;
                        }
                        else
                        {
                            animator.ResetTrigger(variableName);
                            hasBeenApplied = true;
                        }
                        break;
                }
            }
        }
    }
    public VariableData[] animatorVariables;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        for (int i = 0; i < animatorVariables.Length; ++i)
        {
            animatorVariables[i].hasBeenApplied = false;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float normalizedTime = animatorStateInfo.normalizedTime - Mathf.Floor(animatorStateInfo.normalizedTime);

        for (int i = 0; i < animatorVariables.Length; ++i)
        {
            animatorVariables[i].Apply(animator, normalizedTime);
        }
    }
}
