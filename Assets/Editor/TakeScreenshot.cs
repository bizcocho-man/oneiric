using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TakeScreenshot : MonoBehaviour
{
    [MenuItem("Tools/Take Screenshot")]
    static void Screenshot()
    {
        ScreenCapture.CaptureScreenshot("screenshot.png");
    }
}
