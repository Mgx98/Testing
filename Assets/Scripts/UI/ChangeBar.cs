using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBar : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private Sprite healthSprite;
    [SerializeField] private Sprite healthStaminaSprite;

    private void Start()
    {
        player = GameObject.Find("Player Manager").GetComponent<Player>();
    }
    void Update()
    {
        if (player.currentPossessedBody.tag == "Ghost" && gameObject.GetComponent<Image>().sprite != healthSprite) gameObject.GetComponent<Image>().sprite = healthSprite;
        if (player.currentPossessedBody.tag != "Ghost" && gameObject.GetComponent<Image>().sprite != healthStaminaSprite) gameObject.GetComponent<Image>().sprite = healthStaminaSprite;

    }
}
