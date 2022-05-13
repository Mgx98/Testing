using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToCamera : MonoBehaviour
{
    private Transform playerCamera;

    void Awake()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + playerCamera.forward);
    }
}
