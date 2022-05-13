using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Camera minimapCamera;

    [SerializeField] private Image border;
    [SerializeField] private RawImage minimap;

    public float size;
    public float clampOffset;
    public float minimapCameraHeight = 20;

    public bool rotateWithPlayer;
    public bool displayIcons;
    public bool circle;

    private int minimapLayer = 1 << 7;
    private int minimapPointerLayer = 1 << 8;

    void Start()
    {           
        UpdateSettings();
        if (clampOffset > size) clampOffset = size - 1;
    }

    private void Update()
    {
        UpdateSettings();
            
    }

    public void UpdateSettings()
    {
        minimapCamera.orthographicSize = size;

        if (displayIcons) minimapCamera.cullingMask = minimapLayer | minimapPointerLayer;
        else minimapCamera.cullingMask = ~minimapLayer | minimapPointerLayer;

        if (circle)
        {
            border.maskable = true;
            minimap.maskable = true;
        } else
        {
            border.maskable = false;
            minimap.maskable = false;
            rotateWithPlayer = false;
        }
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.currentPossessedBody.transform.position;
        newPosition.y += minimapCameraHeight;
        transform.position = newPosition;

        if (rotateWithPlayer) transform.rotation = Quaternion.Euler(90, player.currentPossessedBody.transform.rotation.eulerAngles.y, 0);
    }   
}
