using CM.Events;
using Entities.Enemies;
using PossessionAbility.Events;
using System.Collections;
using UnityEngine;

namespace PossessionAbility
{
	/// <summary>
	/// Invokes the posession events on raycast hit + key being pressed
	/// </summary>
	[RequireComponent(typeof(RaycastInteraction))]
	public class PossessionInteraction : MonoBehaviour
	{
		public bool IsPossessing { get; private set; }

		public float delay;

		public GameObject enemyPossessedPrefab;

		private RaycastInteraction _raycastInteraction;

		private void Awake()
		{
			_raycastInteraction = GetComponent<RaycastInteraction>();
			_raycastInteraction.OnInteraction += OnInteraction;
			_raycastInteraction.OnRaycastEnter += OnRaycastEnter;
			_raycastInteraction.OnRaycastExit += OnRaycastExit;

			EventManager.AddListener<PossessionStartEvent>(OnPossessionStart);
		}

		private void OnDestroy()
		{
			EventManager.RemoveListener<PossessionStartEvent>(OnPossessionStart);
		}

		private IEnumerator PossessionDelayRoutine(float delay, PossessionSwapEvent possessionSwapEvent)
		{
			yield return new WaitForSeconds(delay);

			EventManager.Trigger(possessionSwapEvent);

			IsPossessing = false;
		}

		private void OnPossessionStart(object eventData)
		{
			PossessionStartEvent possessionStartEvent = eventData as PossessionStartEvent;

			IsPossessing = true;

			PossessionSwapEvent possessionSwapEvent = new PossessionSwapEvent(
				possessionStartEvent.CurrentPossessionObject,
				possessionStartEvent.TargetPossessionObject
			);

			StartCoroutine(PossessionDelayRoutine(possessionStartEvent.Delay, possessionSwapEvent));
		}

		private void OnInteraction(GameObject currentGameObject, GameObject targetGameObject)
		{
			if (IsPossessing)
				return;

			if (currentGameObject.tag != "Ghost")
				return;

			EventManager.Trigger(new AimAtPossessableExitEvent(currentGameObject, targetGameObject));

			Tank tank = targetGameObject.GetComponent<Tank>();

			GameObject newPlayer = Instantiate(tank.possessionPrefab, targetGameObject.transform.position, targetGameObject.transform.rotation);
			FindObjectOfType<Player>().moveSpeed = tank.possessionMoveSpeed;
			Destroy(targetGameObject);

			PossessionAbility.Possess(currentGameObject, newPlayer, delay);
			
		}

		private void OnRaycastEnter(GameObject currentGameObject, GameObject targetGameObject)
		{
			EventManager.Trigger(new AimAtPossessableEnterEvent(currentGameObject, targetGameObject));
		}

		private void OnRaycastExit(GameObject currentGameObject, GameObject targetGameObject)
		{
			EventManager.Trigger(new AimAtPossessableExitEvent(currentGameObject, targetGameObject));
		}
	}
}
