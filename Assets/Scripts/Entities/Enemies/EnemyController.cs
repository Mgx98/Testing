using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entities.Enemies
{
    /// <summary>
    /// This script defines the base of the enemies.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]

    public class EnemyController : EntityController
    {
        public bool lower;
        public Siphon script;
        public Player _player;

    
     
        
       

        

        /// <summary>
        /// Enemy notified state property.
        /// </summary>
        /// <returns> Whether the enemy knows or suspects the player is near. </returns>

        private NavMeshAgent _agent;

       

  


        [SerializeField] bool patrolWaiting;
        [SerializeField] float totalWaitTime = 3f;
        [SerializeField] List<Waypoint> patrolPoints;
        [SerializeField] float switchProb = 0.2f;

        int currentPatrolIndex;
        bool traveling;
        bool waiting;
        bool patrolForward;
        float waitTimer;

       

       

        protected override void Awake()
        {
            base.Awake();

            _agent = gameObject.GetComponent<NavMeshAgent>();
            script = gameObject.GetComponent<Siphon>();
            
        }


        private void Start()
        {
            _player = FindObjectOfType<Player>();
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;

            }
        }

        private void Update()
        {
            if (lower)
            {
                
            }
            if (PlayerInSight() && PlayerInRange())
            {
               
                lower = true;
                Attacking();
                script.isFiring = true;
                


            }
            else if (PlayerInSight() && !PlayerInRange())
            {
                lower = false;
                script.isFiring = false;
                Pursuing();

            }
            else if (!PlayerInSight() && !PlayerInRange())
            {
                lower = false;
                script.isFiring = false;
                Patrolling();
                if (traveling && _agent.remainingDistance <= 1f)
                {
                    traveling = false;

                    if (patrolWaiting)
                    {
                        waiting = true;
                        waitTimer = 0f;
                    }

                    else
                    {
                        ChangePatrolPoint();
                        SetDestination();
                    }
                }

                if (waiting)
                {

                    waitTimer += Time.deltaTime;
                    if (waitTimer >= totalWaitTime)
                    {
                        waiting = false;
                        ChangePatrolPoint();
                        SetDestination();
                    }
                }

            }


        }

        

        

        public override void TakeDamage(int attackDamage)
        {
            EntityStats.Health -= (int)(attackDamage * EntityStats.ArmorShielding);
        }

        protected override void Die()
        {
            Destroy(this);
        }

        protected override void Dodge()
        {
        }

      


       

        private bool PlayerInSight()
        {
            
            return Vector3.Distance(_player.currentPossessedBody.transform.localPosition, transform.localPosition) <
                   EntityStats.Sight;
        }

        private bool PlayerInRange()
        {
            return Vector3.Distance(_player.currentPossessedBody.transform.localPosition, transform.localPosition) <
                   EntityStats.AttackRange;
        }

        private void Patrolling()
        {
            SetDestination();
        }

        public void Attacking()
        {
            _agent.SetDestination(transform.localPosition);
            transform.LookAt(_player.currentPossessedBody.transform.localPosition);



        }

        

        private void SetDestination()
        {
            if (patrolPoints != null)
            {
                Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
                _agent.SetDestination(targetVector);
                traveling = true;
            }
        }

        private void ChangePatrolPoint()
        {
            if (UnityEngine.Random.Range(0f, 1f) <= switchProb)
            {
                patrolForward = !patrolForward;
            }

            if (patrolForward)
            {
                currentPatrolIndex++;
                if (currentPatrolIndex >= patrolPoints.Count)
                {
                    currentPatrolIndex = 0;
                }

            }
            else
            {
                currentPatrolIndex--;
                if (currentPatrolIndex < 0)
                {
                    currentPatrolIndex = patrolPoints.Count - 1;
                }
            }
        }
        private void Pursuing()
        {
            _agent.SetDestination(_player.currentPossessedBody.transform.position);

        }



      



       

        private void OnDrawGizmosSelected()
        {
            var position = transform.position;

            if (!EntityStats) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, EntityStats.AttackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, EntityStats.Sight);
        }

       
    }
}