using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCon : MonoBehaviour
{

    public float speed;
    public int damageToGive;
    public bool doesAreaDamage = false;
    public float areaDamageRange = 10f;
    public ParticleSystem targetHitParticleSystem;
    public ParticleSystem targetHitParticleSystemBig;
    public ParticleSystem groundHitParticleSystem;
    public AudioSource hitSound;
    public AudioSource hitGroundSound;
    private PlayerStamina PlayerStamina;
    

    // Start is called before the first frame update
    void Awake()
    {
        PlayerStamina = GameObject.Find("Player Manager").GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  
        Destroy(gameObject,2);
    }

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStamina.TakeDamage(damageToGive);
            col.gameObject.GetComponent<BodyStamina>().TakeDamage(damageToGive);

            hitSound.transform.parent = null;
            hitSound.Play();

            Instantiate(targetHitParticleSystem, transform.position, Quaternion.identity);

        }
        else if (col.gameObject.tag == "Enemy" && tag != "Enemy")
        {
            if (doesAreaDamage)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, areaDamageRange);

                foreach (Collider collider in colliders)
                {
                    EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();

                    if (!enemyHealth)
                        continue;

                    enemyHealth.TakeDamage(damageToGive);
                    Instantiate(targetHitParticleSystemBig, transform.position, Quaternion.identity);
                }
            }
            else
            {
                EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();

                enemyHealth.TakeDamage(damageToGive);
                Instantiate(targetHitParticleSystem, transform.position, Quaternion.identity);
            }

            hitSound.transform.parent = null;
            hitSound.Play();
        }
        else
        {
            Instantiate(groundHitParticleSystem, transform.position, Quaternion.identity);

            hitGroundSound.transform.parent = null;
            hitGroundSound.Play();
        }
        Destroy(gameObject);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, areaDamageRange);
    }
}
