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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      


        if(isFiring){
            shotCounter-= Time.deltaTime;
            if(shotCounter <=0){
                shotCounter = timeBetween;
                ArrowCon newArrow = Instantiate(arrow,firePoint.position,firePoint.rotation) as ArrowCon;
                newArrow.speed = arrowSpeed;
            }
        } 
        else 
        {
            shotCounter = 0;
        }

    }
}
