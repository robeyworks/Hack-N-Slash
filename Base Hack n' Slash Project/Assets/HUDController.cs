using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Slider healthBar;
    public Slider dashCooldown;
    public Slider hookCooldown;

    public void SetSliderMaxValue(Slider slider, int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetSliderValue(Slider slider, int value)
    {
        slider.value = value;
    }
}
