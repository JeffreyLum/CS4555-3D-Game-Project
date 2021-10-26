using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TimeSlowdown timeSlow;
    public void StartTimeSlow()
    {
        gameObject.SetActive(true);
        timeSlow.PerformSlowMotion();
    }

    public void RestartButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SetSpeedNormal()
    {
        timeSlow.NormalSpeed();
    }
}
