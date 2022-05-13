using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPointer : MonoBehaviour
{
    private Minimap minimap;
    private Player player;
    private SpriteRenderer icon;

    [SerializeField] private Sprite iconSprite;
    [SerializeField] private Sprite pointerSprite;

    private int minimapLayer = 7;
    private int minimapPointerLayer = 8;

    private void Awake()
    {

        minimap = GameObject.Find("Minimap Camera").GetComponent<Minimap>();
        player = GameObject.Find("Player Manager").GetComponent<Player>();
        icon = GetComponent<SpriteRenderer>();

    }
    void Start()
    {
        if (InsideMinimapRadius() && icon.sprite != iconSprite) ChangeToIcon();
        if (!InsideMinimapRadius() && icon.sprite != pointerSprite) ChangeToPointer();
    }

    void Update()
    {
        if (InsideMinimapRadius() && icon.sprite != iconSprite) ChangeToIcon();
        if (!InsideMinimapRadius() && icon.sprite != pointerSprite) ChangeToPointer();
    }

    private void LateUpdate()
    {
        if (!InsideMinimapRadius())
        {
            //Clamp the pointer at the edge of the minimap
            if (minimap.circle) transform.position = player.currentPossessedBody.transform.position + (transform.position - player.currentPossessedBody.transform.position) * (minimap.size - minimap.clampOffset) / Vector3.Distance(player.currentPossessedBody.transform.position, transform.position);
            else transform.position = new Vector3(Mathf.Clamp(transform.position.x, player.currentPossessedBody.transform.position.x - (minimap.size - minimap.clampOffset), player.currentPossessedBody.transform.position.x + (minimap.size - minimap.clampOffset)), transform.position.y, Mathf.Clamp(transform.position.z, player.currentPossessedBody.transform.position.z - (minimap.size - minimap.clampOffset), player.currentPossessedBody.transform.position.z + (minimap.size - minimap.clampOffset)));

            //Rotate the pointer towards the object
            transform.LookAt(player.currentPossessedBody.transform.position, Vector3.up);
            transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 180);
        }
    } 

    private void ChangeToIcon()
    {
        icon.sprite = iconSprite;
        gameObject.layer = minimapLayer;
        transform.localScale = new Vector3(0.1f, 0.1f, 0);
        transform.position = transform.parent.position;
    }

    private void ChangeToPointer()
    {
        icon.sprite = pointerSprite;
        gameObject.layer = minimapPointerLayer;
        transform.localScale = new Vector3(1f, 1f, 0);
    }

    private bool InsideMinimapRadius()
    {
        if (minimap.circle) // if minimap is circulair sized
        {
            if (Vector3.Distance(player.currentPossessedBody.transform.position, new Vector3(transform.parent.position.x, player.currentPossessedBody.transform.position.y, transform.parent.position.z)) < minimap.size - minimap.clampOffset) return true;
            else return false;
        }
        else //if minimap  is square sized
        {
            if (transform.position.x > player.currentPossessedBody.transform.position.x - (minimap.size - minimap.clampOffset) && transform.position.x < player.currentPossessedBody.transform.position.x + (minimap.size - minimap.clampOffset) && transform.position.z > player.currentPossessedBody.transform.position.z - (minimap.size - minimap.clampOffset) && transform.position.z < player.currentPossessedBody.transform.position.z + (minimap.size - minimap.clampOffset)) return true;
            else return false;
        }
    }
}
