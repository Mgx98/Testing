using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCon : MonoBehaviour
{

    public bool isFiring;

    public ArrowCon arrow;
    public float arrowSpeed;

    public float timeBetween;
    private float shotCounter;

    public Transform firePoint;

    public AudioSource audioSource;

    private Player player;

	private void Awake()
	{
        player = FindObjectOfType<Player>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shotCounter > 0)
            shotCounter -= Time.deltaTime;

        if (isFiring){
            if(shotCounter <=0){
                shotCounter = timeBetween;
                audioSource.Play();
                ArrowCon newArrow = Instantiate(arrow,firePoint.position,firePoint.rotation) as ArrowCon;
                if (tag == "Enemy")
				{
                    newArrow.transform.LookAt(player.currentPossessedBody.transform);
                    newArrow.tag = "Enemy";
				}
                newArrow.speed = arrowSpeed;
            }
        }
    }
}
