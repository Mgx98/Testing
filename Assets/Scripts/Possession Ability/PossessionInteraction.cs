using CM.Events;
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

		private RaycastInteraction _raycastInteraction;

		private void Awake()
		{
			_raycastInteraction = GetComponent<RaycastInteraction>();
			_raycastInteraction.OnInteraction += OnInteraction;
			_raycastInteraction.OnRaycastEnter += OnRaycastEnter;
			_raycastInteraction.OnRaycastExit += OnRaycastExit;
		}

		private IEnumerator PossessionDelayRoutine(float delay, PossessionSwapEvent possessionSwapEvent)
		{
			yield return new WaitForSeconds(delay);

			EventManager.Trigger(possessionSwapEvent);

			IsPossessing = false;
		}

		private void OnInteraction(GameObject currentGameObject, GameObject targetGameObject)
		{
			if (IsPossessing)
				return;

			IsPossessing = true;

			PossessionAbility.Possess(currentGameObject, targetGameObject, delay);

			PossessionSwapEvent possessionSwapEvent = new PossessionSwapEvent(
				currentGameObject,
				targetGameObject
			);

			StartCoroutine(PossessionDelayRoutine(delay, possessionSwapEvent));
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
