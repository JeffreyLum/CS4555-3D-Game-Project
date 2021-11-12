using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowdown : MonoBehaviour
{

    public float timeSlowFactor = 0.02f;

    private void Start()
    {
        NormalSpeed();
    }

    private void Update()
    {
    }


    public void PerformSlowMotion()
    {
        Time.timeScale = timeSlowFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void NormalSpeed()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale;
    }
}
