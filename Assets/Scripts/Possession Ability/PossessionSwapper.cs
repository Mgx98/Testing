using CM.Events;
using PossessionAbility.Events;
using UnityEngine;

namespace PossessionAbility
{
	/// <summary>
	/// Swaps two bodies on PossessionSwapEvent
	/// </summary>
	public class PossessionSwapper : MonoBehaviour
	{
		[SerializeField]
		private Player _player;

		[SerializeField]
		private RaycastInteraction _raycastInteraction;

		[SerializeField]
		private PossessionLeaver _possessionLeaver;

		private void Awake()
		{
			EventManager.AddListener<PossessionSwapEvent>(OnPossessionSwap);
		}

		private void OnDestroy()
		{
			EventManager.RemoveListener<PossessionSwapEvent>(OnPossessionSwap);
		}

		private void OnPossessionSwap(object eventData)
		{
			PossessionSwapEvent possessionSwapEvent = eventData as PossessionSwapEvent;

			if (possessionSwapEvent.CurrentPossessionObject.GetComponent<IsGhost>())
				Destroy(possessionSwapEvent.CurrentPossessionObject);

			possessionSwapEvent.CurrentPossessionObject.layer = LayerMask.NameToLayer("Possessable");
			possessionSwapEvent.TargetPossessionObject.layer = 0;
			_player.SetTargetTo(possessionSwapEvent.TargetPossessionObject);
			_player.theBow = possessionSwapEvent.TargetPossessionObject.GetComponentInChildren<BowCon>();

			_raycastInteraction.target = possessionSwapEvent.TargetPossessionObject;
			_possessionLeaver.target = possessionSwapEvent.TargetPossessionObject;
		}
	}
}
