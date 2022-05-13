using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public NavMeshAgent patrole;
    public Player player;
    
    private Rigidbody myrb;
    public LayerMask ground, myPlayer;

    public float health = 5;

   
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
  void Start()
    {
        myrb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
    }
    private void Seek()
    {
        transform.LookAt(player.currentPossessedBody.transform.position);
        patrole = GetComponent<NavMeshAgent>();
    }


    private void Wander()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            patrole.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        patrole.SetDestination(player.currentPossessedBody.transform.position);
    }



     void OnCollisionEnter(Collision other) {
      if(other.gameObject.tag == "Player")
      {
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        Destroy(gameObject);
      }
    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, myPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, myPlayer);

        if (!playerInSightRange && !playerInAttackRange) Wander();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
       
    }
    
}
