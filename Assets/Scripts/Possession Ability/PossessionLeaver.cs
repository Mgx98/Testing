using CM.Events;
using PossessionAbility.Events;
using UnityEngine;

namespace PossessionAbility
{
	/// <summary>
	/// Leaves the possession to a default ghost
	/// </summary>
	public class PossessionLeaver : MonoBehaviour
	{
		public GameObject target;
		public float offset = 3;
		public float delay = 2;

		[SerializeField]
		private GameObject _ghostPrefab;

		private void Awake()
		{
			EventManager.AddListener<PossessionLeaveEvent>(OnPossessionLeave);
		}

		private void OnDestroy()
		{
			EventManager.RemoveListener<PossessionLeaveEvent>(OnPossessionLeave);
		}

		private void OnPossessionLeave(object eventData)
		{
			if (
				GetComponent<PossessionInteraction>().IsPossessing ||
				target.GetComponent<IsGhost>()
			)
				return;

			PossessionLeaveEvent possessionLeaveEvent = eventData as PossessionLeaveEvent;

			GameObject ghost = Instantiate(
				_ghostPrefab,
				possessionLeaveEvent.CurrentPossessionObject.transform.position + possessionLeaveEvent.CurrentPossessionObject.transform.forward * offset,
				possessionLeaveEvent.CurrentPossessionObject.transform.rotation
			);

			FindObjectOfType<Player>().moveSpeed = 20;

			PossessionAbility.Possess(possessionLeaveEvent.CurrentPossessionObject, ghost, possessionLeaveEvent.Delay);
		}
	}
}