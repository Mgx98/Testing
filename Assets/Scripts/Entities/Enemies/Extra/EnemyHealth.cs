using Entities.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public UnityEvent hitEvent;

    [HideInInspector] public float enemyHealth;
    public float maxEnemyHealth = 100;
    public int scoreForKill = 100;

    private UpdateSlider healthBar;
    public bool dieOnStart = false;

    void Start()
    {
        healthBar = this.gameObject.GetComponentInChildren<UpdateSlider>();
        enemyHealth = maxEnemyHealth;
        healthBar.SetMax(maxEnemyHealth);

        if (dieOnStart)
        {
            GetComponent<TankExplosion>().Explode();
            Destroy(this);
            SpawnPoint.currentSpawnablesCount--;
        }
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            GetComponent<TankExplosion>().Explode();
            Destroy(this);
            SpawnPoint.currentSpawnablesCount--;
            TextMeshProUGUI textComponent = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
            int textAsInt = int.Parse(textComponent.text);
            textAsInt += scoreForKill;
            textComponent.text = textAsInt.ToString();

            if (textAsInt > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", textAsInt);
                GameObject.FindGameObjectWithTag("Highscore").GetComponent<TextMeshProUGUI>().text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
                NewHighscore.newHighscore = true;
            }
            else
            {
                NewHighscore.newHighscore = false;
            }

        }
    }
    public void TakeDamage(float damage)
    {
        if (enemyHealth > 0)
        {
            hitEvent.Invoke();
            enemyHealth -= damage;
            if (enemyHealth < 0) enemyHealth = 0;
            healthBar.UpdateDisplay(enemyHealth);
        }
    }

}
