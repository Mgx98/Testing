using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTestEnemy : MonoBehaviour
{

    private PlayerHealth playerHealth;
    private PlayerStamina playerStamina;
    //private BodyStamina bodyStamina;
    //private EnemyHealth enemyHealth;
    void Awake()
    {
        playerHealth = GameObject.Find("Player Manager").GetComponent<PlayerHealth>();
        playerStamina = GameObject.Find("Player Manager").GetComponent<PlayerStamina>();
        //bodyStamina = GameObject.Find("Cube").GetComponent<BodyStamina>();
        //enemyHealth = GetComponentInChildren<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //playerHealth.isGhost = true;
        playerStamina.TakeDamage(10);
        //enemyHealth.TakeDamage(10);
        other.GetComponent<BodyStamina>().TakeDamage(10);

    }

    private void OnTriggerExit(Collider other)
    {
        //playerHealth.isGhost = false;
    }
}
