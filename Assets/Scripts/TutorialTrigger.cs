using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private Canvas tutorialCanvas;
    [SerializeField] private Text text1;
    [SerializeField] private Image keyboardButton;
    [SerializeField] private Text separator;
    [SerializeField] private Image gamepadButton;
    [SerializeField] private Text text2;

    private void OnTriggerEnter(Collider other)
    {
        tutorialCanvas.enabled = true;
    }
}
