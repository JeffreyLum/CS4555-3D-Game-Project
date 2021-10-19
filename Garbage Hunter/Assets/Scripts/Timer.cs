using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{

     float currentTime = 0f;
     float startingTime = 60f;

    [SerializeField] TMPro.TextMeshProUGUI countdownText;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0.00");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
