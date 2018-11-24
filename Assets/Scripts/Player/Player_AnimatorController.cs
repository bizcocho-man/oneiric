using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player_ShapeController))]
public class Player_AnimatorController : MonoBehaviour
{
    [HideInInspector] public float movementSpeed;
    [HideInInspector] public float animatorSpeed;
    [HideInInspector] public bool isOnGround;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canJump;
    [HideInInspector] public bool canLand;
    [HideInInspector] public bool isJumping;

    private Animator animator;

    // Hashes
    private int hash_MovementSpeed = Animator.StringToHash("MovementSpeed");
    private int hash_AnimatorSpeed = Animator.StringToHash("AnimatorSpeed");
    private int hash_IsOnGround = Animator.StringToHash("IsOnGround");
    private int hash_CanMove = Animator.StringToHash("CanMove");
    private int hash_CanJump = Animator.StringToHash("CanJump");
    private int hash_CanLand = Animator.StringToHash("CanLand");
    private int hash_IsJumping = Animator.StringToHash("IsJumping");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Update animator variables
        if (animator == null)
        {
            return;
        }

        animator.SetFloat(hash_MovementSpeed, movementSpeed);
        animator.SetFloat(hash_AnimatorSpeed, animatorSpeed);
        animator.SetBool(hash_IsOnGround, isOnGround);
        animator.SetBool(hash_CanMove, canMove);
        animator.SetBool(hash_CanJump, canJump);
        animator.SetBool(hash_CanLand, canLand);
        animator.SetBool(hash_IsJumping, isJumping);
    }
}
