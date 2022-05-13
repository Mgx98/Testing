using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entities.Enemies
{

	[RequireComponent(typeof(NavMeshAgent))]
	public class Tank : MonoBehaviour
	{


		public Player _player;
		public BowCon cannon;
		public GameObject possessionPrefab;
		public float possessionMoveSpeed = 20;

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



		private void Start()
		{


			_agent = gameObject.GetComponent<NavMeshAgent>();
			_player = FindObjectOfType<Player>();
			GameObject[] allwaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

			patrolPoints = new List<Waypoint>();

			for (int i = 0; i < allwaypoints.Length; i++)
			{

				Waypoint nextpoint = allwaypoints[i].GetComponent<Waypoint>();

				if (nextpoint != null)
				{
					patrolPoints.Add(nextpoint);
				}
			}


			if (patrolPoints != null && patrolPoints.Count >= 2)
			{
				currentPatrolIndex = 0;

			}
		}

		private void Update()
		{

			if (PlayerInSight() && PlayerInRange())
			{


				Attacking();




			}
			else if (PlayerInSight() && !PlayerInRange())
			{

				Pursuing();

			}
			else if (!PlayerInSight() && !PlayerInRange())
			{

				Patrolling();
				if (traveling && _agent.remainingDistance <= 20f)
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



		private bool PlayerInSight()
		{

			return Vector3.Distance(_player.currentPossessedBody.transform.localPosition, transform.localPosition) <
				   60;
		}

		private bool PlayerInRange()
		{
			return Vector3.Distance(_player.currentPossessedBody.transform.localPosition, transform.localPosition) <
				   30;
		}

		private void Patrolling()
		{
			SetDestination();
			cannon.isFiring = false;
		}

		public void Attacking()
		{
			Vector3 lookPosition = _player.currentPossessedBody.transform.localPosition - transform.localPosition;

			lookPosition.y = 0;
	
			_agent.SetDestination(transform.localPosition);
			transform.rotation = Quaternion.LookRotation(lookPosition);
			cannon.isFiring = true;
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
			cannon.isFiring = false;

		}

	}
}