using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager _instance;
    public static UI_Manager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] GameObject UI;
    [SerializeField] GameObject UI_Option;
    [SerializeField] GameObject UI_Credits;

    [SerializeField] Button currentButton;

    private Animation anim;

    private bool isButtonReleased = true;
    private bool isOptionEnabled = false;
    private bool isCreditsEnabled = false;
    private bool isFirstTime = true;

    public bool isPausedGame = true;

    private void Start()
    {
        //Disable mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize button
        SetColorButton(true);

        anim = UI.GetComponent<Animation>();

        Pause();
    }

    // Get Inputs
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (!isPausedGame)
        {
            return;
        }

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
        Resume();
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

    private void Pause()
    { 
        isPausedGame = true;
        UI.SetActive(true);
        anim.Play("FadeUIIn");
    }

    public void Resume()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            anim.Play("FadeUIInit");
            Time.timeScale = 1f;

            return;
        }

        Time.timeScale = 1f;
        isPausedGame = false;
        anim.Play("FadeUIOut");
    }
}
