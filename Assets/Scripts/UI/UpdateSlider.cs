using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMax(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void UpdateDisplay(float value)
    {
        slider.value = value;
    }
}
