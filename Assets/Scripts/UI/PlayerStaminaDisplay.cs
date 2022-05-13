using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaDisplay : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxPlayerStamina(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void UpdatePlayerStaminaDisplay(float max)
    {
        slider.value = max;
    }
}
