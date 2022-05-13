using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerIcon : MonoBehaviour
{
    [SerializeField] private Player player;

    void Update()
    {
        transform.position = player.currentPossessedBody.transform.position;
        transform.rotation = Quaternion.Euler(90, player.currentPossessedBody.transform.rotation.eulerAngles.y, 0);
    }
}
