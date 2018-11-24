using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCollisionChangeScene : MonoBehaviour
{
    public string nameScene;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nameScene);
    }
}
