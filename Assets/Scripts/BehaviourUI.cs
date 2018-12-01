using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BehaviourUI : MonoBehaviour
{
    public GameObject player; 
    [SerializeField] Image image;
    [SerializeField] Text[] texts;

    private Color unhovered = new Color32(127, 123, 130, 90);

    public void EndAnimationInit()
    {
        UI_Manager.Instance.Resume();
        UI_Manager.Instance.canPause = true;

        gameObject.SetActive(false); 

        /*foreach (Text text in texts)
        {
            text.color = unhovered;
        }*/

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        UI_Manager.Instance.canReceiveInput = true;
        UI_Manager.Instance.isPausedGame = false;
    }

    public void EndAnimationIn()
    {
        gameObject.SetActive(true);
    }

    public void EndAnimationOut()
    {
        UI_Manager.Instance.UI_Main.SetActive(false);
        UI_Manager.Instance.canReceiveInput = true;
    }

    public void EndAnimationInDeath()
    {
        player.transform.position = CheckpointManager.Instance.GetCurrentCheckpoint().transform.position;
        CheckpointManager.Instance.GetCurrentCheckpoint().RestartLevel();
        UI_Manager.Instance.FadeOutDeath();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
