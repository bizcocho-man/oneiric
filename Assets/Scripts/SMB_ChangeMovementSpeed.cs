using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_ChangeMovementSpeed : StateMachineBehaviour
{
    [System.Serializable]
    private struct SpeedData
    {
        public float speedValue;
        public float movementSpeed;
    }

    [SerializeField] private SpeedData[] speedData;

    private Player_MovementController movementController;
    private int speedHash = Animator.StringToHash("Speed");

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (movementController == null)
        {
            movementController = animator.transform.gameObject.GetComponent<Player_MovementController>();
        }

        if (movementController != null)
        {
            float speed = Mathf.Abs(animator.GetFloat(speedHash));

            for (int i = 0; i < speedData.Length; ++i)
            {
                if (speed >= speedData[i].speedValue)
                {
                    movementController.movementSpeed = speedData[i].movementSpeed;
                }
            }
        }
    }
}
