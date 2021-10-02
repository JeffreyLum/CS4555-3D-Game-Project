using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string[] Difficulty = { "Easy", "Medium", "Hard", "Tutorial" };
    public int Option = 1;
    public GameObject MM;
    public GameObject DS;


    public void OpenDiff()
    {
        Debug.Log("Scene Launched");
        MM.SetActive(false);
        DS.SetActive(true);
    }

    public void setEasy()
    {
        Option = 1;
        PlayGame();
    }

    public void setMedium()
    {
        Option = 2;
        PlayGame();
    }

    public void setHard()
    {
        Option = 3;
        PlayGame();
    }


    public void PlayGame()
    {
        Debug.Log("Scene Launched");
        int a = Option % Difficulty.Length;
        switch (a) 
        {
            default:
            case 1:
                Debug.Log("Scene Launched 1");
                break;
            case 2:
                Debug.Log("Scene Launched 2");
                break;
            case 3:
                Debug.Log("Scene Launched 3");
                break;
            case 4:
                Debug.Log("Scene Launched 4");
                break;
        }
        SceneManager.LoadScene("EasyLevel");
    }

    public void QuitGame()
    {
        Debug.Log("Exited Game");
        Application.Quit();
    }
}
