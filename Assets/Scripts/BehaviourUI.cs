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

    public void EndAnimationInit()
    {
        UI_Manager.Instance.Resume();
        UI_Manager.Instance.canPause = true;

        gameObject.SetActive(false);

        foreach (Text text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);  
    }

    public void EndAnimationIn()
    {
        gameObject.SetActive(true);
    }

    public void EndAnimationOut()
    {
        UI_Manager.Instance.UI_Main.SetActive(false);
    }

    public void EndAnimationInDeath()
    {
        player.transform.position = CheckpointManager.Instance.GetCurrentCheckpoint().transform.position;
        UI_Manager.Instance.FadeOutDeath();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
