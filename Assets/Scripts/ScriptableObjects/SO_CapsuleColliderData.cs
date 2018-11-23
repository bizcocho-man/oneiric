using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CapsuleColliderData", menuName = "ScriptableObjects/CapsuleColliderData")]
public class SO_CapsuleColliderData : ScriptableObject
{
    public Vector3 center;
    public float radius;
    public float height;
}
