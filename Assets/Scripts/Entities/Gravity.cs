using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    private Vector3 velocity;
    public float speed = 0.1f;
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        velocity.y -= speed * Time.deltaTime;
        controller.Move(velocity);
        if (controller.isGrounded) velocity.y = 0;

    }
}
