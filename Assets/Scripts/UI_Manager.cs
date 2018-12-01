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

    public GameObject UI;
    public GameObject UI_Main;
    [SerializeField] GameObject UI_Option;
    [SerializeField] GameObject UI_Credits;
    public GameObject UI_Death;
    [SerializeField] GameObject UI_End;
    public GameObject UI_Tutorial;

    [SerializeField] Button currentButton;

    [SerializeField] GameObject Camera;

    private Animation anim;

    private bool isButtonReleased = true;
    private bool isOptionEnabled = false;
    private bool isCreditsEnabled = false;
    private bool isFirstTime = true;

    public bool isDeactivateInput = false;

    [HideInInspector] public bool canReceiveInput = true;
    [HideInInspector] public bool canPause = false;
    [HideInInspector] public bool isPausedGame = true;

    [SerializeField] Image KeyboardButton;
    [SerializeField] Image ControllerButton;
    [SerializeField] Text Action;

    private Color hovered = new Color32(141, 170, 145, 255);
    private Color unhovered = new Color32(127, 123, 130, 190);

    private void Start()
    {
        //Disable mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize button
        SetColorButton(true);

        anim = UI.GetComponent<Animation>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Pause();
        }
        else
        {
            isFirstTime = false;
            canReceiveInput = true;
            FinishInit();
            canPause = true;
            Resume();
            Audio_Manager.Instance.InitializeAudio();
        }
    }

    // Get Inputs
    private void Update()
    {
        if (isDeactivateInput)
        {
            return;
        }

        if (Input.GetButtonDown("Pause") && canPause && !UI_Credits.activeInHierarchy && !UI_Option.activeInHierarchy)
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

        if (isButtonReleased == false && Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Vertical") == 0 && !Input.GetButtonDown("Fire1"))
        {
            isButtonReleased = true;
        }
        
        if (Input.GetAxisRaw("Vertical") < 0 && isButtonReleased && isOptionEnabled == false && isCreditsEnabled == false)
        {
            SetCurrentButton(false);

            isButtonReleased = false;
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && isButtonReleased && isOptionEnabled == false && isCreditsEnabled == false)
        {
            SetCurrentButton(true);

            isButtonReleased = false;
        }
        else if (Input.GetButtonDown("Submit") && isButtonReleased)
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
        currentButton.GetComponentInChildren<Text>().color = isActivated ? hovered : unhovered;
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
        canReceiveInput = false;
        isPausedGame = true;
        UI.SetActive(true);
        UI_Main.SetActive(true);
        anim.Play("FadeUIIn");
    }

    public void Resume()
    {
        if (isFirstTime)
        {
            isDeactivateInput = true;
            Camera.SetActive(true);
            isFirstTime = false;
            anim.Play("FadeUIInit");
            Audio_Manager.Instance.InitializeAudio();
            //Time.timeScale = 1f;

            return;
        }

        //Time.timeScale = 1f;
        isPausedGame = false;
        canPause = true;
        anim.Play("FadeUIOut");
    }

    public void FadeInDeath()
    {
        UI_Main.SetActive(false);
        UI.SetActive(true);
        UI_Death.SetActive(true);
        anim.Play("FadeInDeath");
    }

    public void FadeOutDeath()
    {
        anim.Play("FadeOutDeath");
    }

    public void EndGame()
    {
        UI_End.SetActive(true);
        anim.Play("EndAnimation");
    }

    public void FinishInit()
    {
        isDeactivateInput = false;
        isPausedGame = true;
    }

    public void ShowTutorial(Sprite keyboardButtonImage, Sprite controllerButtonImage, string actionText)
    {
        UI_Tutorial.SetActive(true);
        anim.Play("FadeInTutorial");    

        KeyboardButton.sprite = keyboardButtonImage;
        KeyboardButton.preserveAspect = true;

        ControllerButton.sprite = controllerButtonImage;
        ControllerButton.preserveAspect = true;

        Action.text = actionText;
    }

    public void HideTutorial()
    {
        anim.Play("FadeOutTutorial"); 
    }
}
