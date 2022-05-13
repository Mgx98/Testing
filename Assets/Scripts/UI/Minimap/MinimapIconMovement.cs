using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconMovement : MonoBehaviour
{
    [SerializeField] private Camera minimapCamera;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, minimapCamera.transform.position.y - minimapCamera.GetComponent<Minimap>().minimapCameraHeight, transform.position.z);
    }
}
