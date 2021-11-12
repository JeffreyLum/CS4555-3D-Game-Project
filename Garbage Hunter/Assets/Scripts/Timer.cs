using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{

    public GameOver gameOver;
    float currentTime = 0f;
    float TimeLimit = 360f;
    public MouseLook mouselook;

    [SerializeField] TMPro.TextMeshProUGUI countdownText;

    private void Start()
    {
        currentTime = TimeLimit;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0.00");

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOver.StartTimeSlow();
        mouselook.CursorUnlock();
    }

}
