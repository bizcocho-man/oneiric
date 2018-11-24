using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraTarget_FollowPlayer))]
public class CameraTarget_FollowPlayerEditor : Editor
{
    private CameraTarget_FollowPlayer targetScript;
    private Color color_OriginalBackgroundColor;
    private Color32 color_FlatGreen;

    private void Awake()
    {
        targetScript = (CameraTarget_FollowPlayer)target;
        color_OriginalBackgroundColor = GUI.backgroundColor;
        color_FlatGreen = new Color32(20, 209, 161, 255);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUI.backgroundColor = color_FlatGreen;
        if (GUILayout.Button("Set position", GUILayout.MaxWidth(100), GUILayout.Height(25)))
        {
            targetScript.transform.position = targetScript.target.position + targetScript.localOffset;
        }
        GUI.backgroundColor = color_OriginalBackgroundColor;

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
    }
}
