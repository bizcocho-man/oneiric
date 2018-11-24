using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_ModifyAnimatorVariables : StateMachineBehaviour
{
    [System.Serializable]
    public struct VariableData
    {
        [Range(0.0f, 1.0f)] public float animationPercentage;

        public enum VariableType { Float, Int, Bool, Trigger }
        public VariableType variableType;

        public string variableName;

        public float floatValue;
        public int intValue;
        public bool boolValue;
        public bool triggerValue;

        public void Apply(Animator animator, float normalizedTime)
        {
            if (normalizedTime >= animationPercentage)
            {
                switch (variableType)
                {
                    case VariableType.Float:
                        animator.SetFloat(variableName, floatValue);
                        break;
                    case VariableType.Int:
                        animator.SetInteger(variableName, intValue);
                        break;
                    case VariableType.Bool:
                        animator.SetBool(variableName, boolValue);
                        break;
                    case VariableType.Trigger:
                        if (triggerValue)
                        {
                            animator.SetTrigger(variableName);
                        }
                        else
                        {
                            animator.ResetTrigger(variableName);
                        }
                        break;
                }
            }
        }
    }
    public VariableData[] animatorVariables;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float normalizedTime = animatorStateInfo.normalizedTime - Mathf.Floor(animatorStateInfo.normalizedTime);

        for (int i = 0; i < animatorVariables.Length; ++i)
        {
            animatorVariables[i].Apply(animator, normalizedTime);
        }
    }
}
