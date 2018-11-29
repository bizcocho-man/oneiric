using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject UI_Option;
    [SerializeField] GameObject UI_Credits;

    [SerializeField] Button currentButton;

    private bool isButtonReleased = true;
    private bool isOptionEnabled = false;
    private bool isCreditsEnabled = false;

    private void Start()
    {
        //Disable mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize button
        SetColorButton(true);
    }

    // Get Inputs
    private void Update()
    {
        if (isButtonReleased == false && Input.GetKey(KeyCode.DownArrow) == false && Input.GetKey(KeyCode.DownArrow) == false && Input.GetKey(KeyCode.LeftControl) == false)
        {
            isButtonReleased = true;
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && isButtonReleased && isOptionEnabled == false && isCreditsEnabled == false)
        {
            SetCurrentButton(false);

            isButtonReleased = false;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isButtonReleased && isOptionEnabled == false && isCreditsEnabled == false)
        {
            SetCurrentButton(true);

            isButtonReleased = false;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && isButtonReleased)
        {
            currentButton.onClick.Invoke();
            isButtonReleased = false;
        }
    }

    void SetCurrentButton(bool isTopButton)
    {
        SetColorButton(false);

        if (isTopButton)
        {
            currentButton = currentButton.navigation.selectOnUp.GetComponent<Button>();
        }
        else
        {
            currentButton = currentButton.navigation.selectOnDown.GetComponent<Button>();
        }

        SetColorButton(true);
    }

    void SetColorButton(bool isActivated)
    {
        currentButton.GetComponentInChildren<Text>().color = isActivated ? Color.cyan : Color.white;
    }

    public void ClickPlay()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void ClickControls()
    {
        isOptionEnabled = !isOptionEnabled;
        UI_Option.SetActive(isOptionEnabled);
    }

    public void ClickCredits()
    {
        isCreditsEnabled = !isCreditsEnabled;
        UI_Credits.SetActive(isCreditsEnabled);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
