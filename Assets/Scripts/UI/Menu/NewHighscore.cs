using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewHighscore : MonoBehaviour
{
    [HideInInspector] static public bool newHighscore;
    [SerializeField] private GameObject newHighscoreText;
    [SerializeField] private GameObject confetti;
    void Start()
    {
        if (newHighscore)
        {
            newHighscoreText.SetActive(true);
            confetti.SetActive(true);
        }
        else
        {
            newHighscoreText.SetActive(false);
            confetti.SetActive(false);
        }
    }
}
