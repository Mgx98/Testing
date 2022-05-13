using UnityEngine;

namespace PossessionAbility
{
	/// <summary>
	/// Adds a raycast to a specified object which triggers an event on pressing a key while the raycast hits
	/// </summary>
	public class RaycastInteraction : MonoBehaviour
	{
		public GameObject target;
		public float maxRange = Mathf.Infinity;
		public KeyCode interactionKey;
		public LayerMask interactionMask;

		public delegate void InteractionEvent(GameObject currentGameObject, GameObject targetGameObject);

		public event InteractionEvent OnInteraction;
		public event InteractionEvent OnRaycastEnter;
		public event InteractionEvent OnRaycastExit;

		private GameObject _raycastHitObject;

#if DEBUG

		[Header("Gizmos")]

		public float maxGizmosRange = 50f;
		public Color gizmosColor = Color.green;

#endif

		private void Update()
		{
			// Interact on interaction key being pressed
			if (Input.GetKeyDown(interactionKey) && _raycastHitObject)
			{
				OnInteraction?.Invoke(target, _raycastHitObject);
			}
		}

		private void FixedUpdate()
		{
			RaycastHit hit;
			bool hasRaycasthit = Physics.Raycast(target.transform.position, target.transform.TransformDirection(Vector3.forward), out hit, maxRange, interactionMask);

			// Raycast exit event when the raycast leaves the current raycast hit
			if (
				(_raycastHitObject != null) &&
				(
					(!hasRaycasthit) ||
					(_raycastHitObject != hit.transform.gameObject)
				)
			)
			{
				OnRaycastExit?.Invoke(target, _raycastHitObject);
				_raycastHitObject = null;
			}

			if (hasRaycasthit)
			{
				// Raycast enter event when there is a new raycast hit
				if (_raycastHitObject != hit.transform.gameObject)
					OnRaycastEnter?.Invoke(target, hit.transform.gameObject);

				_raycastHitObject = hit.transform.gameObject;
			}
		}

#if DEBUG

		private void OnDrawGizmos()
		{
			// Draw interaction ray
			Vector3 direction = target.transform.forward * ((maxRange >= maxGizmosRange) ? maxGizmosRange : maxRange);
			Gizmos.color = gizmosColor;
			Gizmos.DrawRay(target.transform.position, direction);
		}

#endif

	}
}