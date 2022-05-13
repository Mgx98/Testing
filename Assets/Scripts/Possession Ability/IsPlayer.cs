using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player Manager").GetComponent<Player>();
    }
    void Update()
    {
        if (this.gameObject == player.currentPossessedBody) this.tag = "Player";
        else this.tag = "Body";
    }
}
