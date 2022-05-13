using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyStamina : MonoBehaviour
{
    [HideInInspector] public float bodyStamina;
    public float maxBodyStamina = 100;

    private PlayerStamina playerStamina;
    private UpdateSlider staminaBar;
    private void Start()
    {
        playerStamina = GameObject.Find("Player Manager").GetComponent<PlayerStamina>();
        staminaBar = gameObject.GetComponentInChildren<UpdateSlider>();
        bodyStamina = maxBodyStamina;
        staminaBar.SetMax(maxBodyStamina);
    }

    private void Update()
    {
        if (this.tag == "Player")
        {
            bodyStamina = playerStamina.stamina;
            staminaBar.UpdateDisplay(bodyStamina);
            staminaBar.gameObject.SetActive(false);
        }
        else
        {
            staminaBar.gameObject.SetActive(true);
            staminaBar.UpdateDisplay(bodyStamina);
        }
    }


    public void TakeDamage(float damage)
    {
        if (bodyStamina > 0)
        {
            bodyStamina -= damage;
            if (bodyStamina < 0) bodyStamina = 0;
            staminaBar.UpdateDisplay(bodyStamina);
        }
    }
}
