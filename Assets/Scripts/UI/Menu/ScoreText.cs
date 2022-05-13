using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + PlayerDied.score;
    }
}
