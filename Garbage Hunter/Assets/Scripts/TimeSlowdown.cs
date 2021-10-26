using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowdown : MonoBehaviour
{

    public float timeSlowFactor = 0.02f;

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
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
}
