using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerIcon : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player Manager").GetComponent<Player>();
    }

    void Update()
    {
        transform.position = player.currentPossessedBody.transform.position;
        transform.rotation = Quaternion.Euler(90, player.currentPossessedBody.transform.rotation.eulerAngles.y, 0);
    }
}
