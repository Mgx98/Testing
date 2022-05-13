using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCon : MonoBehaviour
{

    public float speed;
    public int damageToGive;
    public Movement thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  
      Destroy(gameObject,2);
    }

    void OnCollisionEnter(Collision other) {
      if(other.gameObject.tag == "Enemy")
      {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageToGive);
            Destroy(gameObject);
      }
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToGive);
            Destroy(gameObject);
        }

        if (other.gameObject.gameObject.layer == LayerMask.NameToLayer("Possessable"))
      {
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToGive);
        Destroy(gameObject);
      }
    }
}
