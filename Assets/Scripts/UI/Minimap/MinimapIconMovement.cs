using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconMovement : MonoBehaviour
{
    private Camera minimapCamera;
    private Minimap minimap;

    private void Awake()
    {
        minimapCamera = GameObject.Find("Minimap Camera").GetComponent<Camera>();
        minimap = minimapCamera.GetComponent<Minimap>();
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, minimapCamera.transform.position.y - minimap.minimapCameraHeight, transform.position.z);
    }
}
