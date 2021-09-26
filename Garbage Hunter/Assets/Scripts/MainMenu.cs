using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("Scene Launched");
        SceneManager.LoadScene("EasyLevel");
    }

    public void QuitGame()
    {
        Debug.Log("Exited Game");
        Application.Quit();
    }
}
