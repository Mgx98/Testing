using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{

    private Rigidbody myrb;
    public float mSpeed;

    public Player thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        myrb = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
           
        transform.LookAt(thePlayer.currentPossessedBody.transform.position);


    }

     void FixedUpdate() {
        myrb.velocity = (transform.forward * mSpeed);
    }

     void OnCollisionEnter(Collision other) {
      if(other.gameObject.tag == "Player")
      {
          other.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        Destroy(gameObject);
      }
    }
}
