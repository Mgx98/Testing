using Entities.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private int currentHealth;
    EnemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        enemy = FindObjectOfType<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
       if(currentHealth<=0){

            enemy.lower = false;
           Destroy(gameObject);
       } 
    }

    public void TakeDamage(int damage)
    {
        currentHealth-=damage;

    }
}
