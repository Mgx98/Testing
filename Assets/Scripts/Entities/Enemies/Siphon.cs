using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Enemies;

public class Siphon : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFiring;

    public Life life;
    public float cubeSpeed;

    public float timeBetween;
    private float shotCounter;

    public Transform firePoint;
    public Player player;




    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {

        firePoint = player.currentPossessedBody.transform;


        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetween;
                Life newLife = Instantiate(life, firePoint.position, firePoint.rotation) as Life;
                newLife.speed = cubeSpeed;
            }
        }
        else
        {
            shotCounter = 0;
        }

    }
}