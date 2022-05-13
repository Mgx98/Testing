using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tArcher : MonoBehaviour
{
     private Rigidbody myRig;
    public BowCon theBow;

    public Player thePlayer;

    // Start is called before the first frame update
    void Start()
    {
          myRig = GetComponent<Rigidbody>();
       
       


    }

    // Update is called once per frame
    void Update()
    {
        thePlayer = FindObjectOfType<Player>();
        transform.LookAt(thePlayer.currentPossessedBody.transform.position);
        theBow.isFiring = true;
        
    }
}
