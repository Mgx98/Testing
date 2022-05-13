using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    private Vector3 velocity;
    private float speed = 5;

    [SerializeField] private Player player;
    private CharacterController controller;
     

    // Update is called once per frame
    void Update()
    {
        velocity = transform.forward * Input.GetAxis("Vertical") * speed;
        controller = player.currentPossessedBody.GetComponent<CharacterController>();
        velocity.z = 0;
        controller.SimpleMove(velocity);
    }
}
