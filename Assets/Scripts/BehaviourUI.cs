using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourUI : MonoBehaviour
{
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
        gameObject.SetActive(false);
    }
}
