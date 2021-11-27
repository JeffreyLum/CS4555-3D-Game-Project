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
    public GameObject TR;

    public GameObject p1;
    public GameObject p2;

    public int page = 1;
    public int mpage = 2;


    public void OpenDiff()
    {
        Debug.Log("Scene Launched");
        MM.SetActive(false);
        DS.SetActive(true);
    }

    public void OpenTutorial()
    {
        MM.SetActive(false);
        DS.SetActive(false);
        TR.SetActive(true);
        page = 1;
        openpage();
    }

    public void OpenMM()
    {
        MM.SetActive(true);
        DS.SetActive(false);
        TR.SetActive(false);
    }

    public void openpage()
    {
        switch (page)
        {
            default:
                break;
            case 1:
                p1.SetActive(true);
                p2.SetActive(false);
                break;
            case 2:
                p1.SetActive(false);
                p2.SetActive(true);
                break;
        }
    }

    public void nextpage()
    {
        if (page < mpage)
        {
            page += 1;
        }
        openpage();
    }

    public void prevpage()
    {
        if (page > 0)
        {
            page -= 1;
        }
        openpage();
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
