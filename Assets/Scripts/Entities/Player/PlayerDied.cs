using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDied : MonoBehaviour
{
    private PlayerHealth playerHealth;
    static public int score;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (playerOutOfHealth()) playerDies();
    }

    public void playerDies()
    {
        TextMeshProUGUI textComponent = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        score = int.Parse(textComponent.text);
        SceneManager.LoadScene("DeadScreen");
        Cursor.visible = true;
    }

    public bool playerOutOfHealth()
    {
        if (playerHealth.health <= 0) return true;
        else return false;
    }
}