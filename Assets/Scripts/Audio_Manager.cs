using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    private static Audio_Manager _instance;
    public static Audio_Manager Instance { get { return _instance; } }

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

    [SerializeField] AudioSource audioLoop;
    [SerializeField] AudioSource audio1;
    [SerializeField] AudioSource audio2;

    public void InitializeAudio()
    {
        audioLoop.Play();

        StartCoroutine(RandomAudio());
    }

    private IEnumerator RandomAudio()
    {
        yield return new WaitForSeconds(Random.Range(15, 30));

        if (Random.Range(0.0f, 2.0f) >= 1)
        {
            audio1.Play();
        }
        else
        {
            audio2.Play();
        }
    }
}
