using Entities.Enemies;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Life : MonoBehaviour
{

    public int damageToGive;
    public float speed;

    private NavMeshAgent agent;

    

    // Start is called before the first frame update
    void Start()
    {
        agent = FindObjectOfType<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(agent.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject,2);
    }








}
