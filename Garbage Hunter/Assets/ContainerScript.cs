using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerScript : MonoBehaviour
{

    public Slider slider;
    public Text textDisplay;

    public void setMax(int max)
    {
        slider.maxValue = max;
        textDisplay.text = "" + slider.value + "/" + slider.maxValue;
    } 

    public void setValue(int value)
    {
        slider.value = value;
        textDisplay.text = ""+ slider.value + "/" + slider.maxValue;
    }

    public void addValue()
    {
        slider.value += 1;
        textDisplay.text = "" + slider.value + "/" + slider.maxValue;
    }


}
