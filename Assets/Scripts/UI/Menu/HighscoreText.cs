using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }
}
