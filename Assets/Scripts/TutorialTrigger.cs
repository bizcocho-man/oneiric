using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private Sprite keyboardButton;
    [SerializeField] private Sprite gamepadButton;
    [SerializeField] private string action;

    private void OnTriggerEnter(Collider other)
    {
        UI_Manager.Instance.ShowTutorial(keyboardButton, gamepadButton, action);
    }

    private void OnTriggerExit(Collider other)
    {
        UI_Manager.Instance.HideTutorial();
    }
}
