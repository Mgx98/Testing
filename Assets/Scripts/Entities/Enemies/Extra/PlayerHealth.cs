using Entities;
using Entities.Enemies;
using PossessionGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   
    [Range(0, 999)] [SerializeField] public int health = 100;
    public float currentHealth;
    public EnemyController enemy;

    [SerializeField] public HealthDisplay healthDisplayer;
  
    void Start()
    {

        healthDisplayer = FindObjectOfType<HealthDisplay>();
        enemy = FindObjectOfType<EnemyController>();
        currentHealth = health;
        healthDisplayer.SetMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
       if(currentHealth<=0){
           
          // Destroy(gameObject);
       }

        if (enemy.lower)
        {
            TakeDamage(0.1f);
        }

    }

    public void TakeDamage(float damage)
    {
        if (health < 0) health = 0;
        currentHealth -= damage;
        healthDisplayer.UpdateHealthDisplay(currentHealth);


    }

    public void heal(float restore)
    {

        currentHealth += restore;

    }


}
