using Entities;
using Entities.Enemies;
using PossessionGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float amountOfDamage;
    public float amountOfHeal;
    public float maxHealth;
    [HideInInspector] public float health;
    
    private UpdateSlider healthDisplayer;
    private PlayerStamina playerStamina;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Highscore").GetComponent<TextMeshProUGUI>().text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
        healthDisplayer = GameObject.Find("Health Bar").GetComponent<UpdateSlider>();
        health = maxHealth;
        healthDisplayer.SetMax(health);
    }
    void Update()
    {
        playerStamina = GameObject.Find("Player Manager").GetComponent<PlayerStamina>();
        if (playerStamina.isGhost) TakeDamage(amountOfDamage * Time.deltaTime);
        else Heal(amountOfHeal * Time.deltaTime) ;
    }


    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health < 0) health = 0;
            healthDisplayer.UpdateDisplay(health);
        }
    }

    public void Heal(float restore)
    {
        if (health < maxHealth)
        {
            health += restore;
            healthDisplayer.UpdateDisplay(health);
        }
    }



}
