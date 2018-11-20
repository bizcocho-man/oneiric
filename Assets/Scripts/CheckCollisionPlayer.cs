using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCollisionPlayer : MonoBehaviour
{
    public string nameScene;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        SceneManager.LoadScene(nameScene);
    }
}
