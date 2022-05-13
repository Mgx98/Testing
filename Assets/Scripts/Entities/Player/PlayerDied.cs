using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDied : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    void Update()
    {
        if (playerOutOfHealth()) playerDies();
    }

    public void playerDies()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool playerOutOfHealth()
    {
        if (playerHealth.health <= 0) return true;
        else return false;
    }
}
