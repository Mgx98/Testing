using CM.Events;
using PossessionAbility.Events;
using UnityEngine;

namespace PossessionAbility
{
	public class PossessionRestructObject : MonoBehaviour
	{
		private void Awake()
		{
			EventManager.AddListener<PossessionStartEvent>(OnPossessionStart);
		}

		private void OnDestroy()
		{
			EventManager.RemoveListener<PossessionStartEvent>(OnPossessionStart);
		}

		private void OnPossessionStart(object eventData)
		{
			PossessionStartEvent possessionStartEvent = eventData as PossessionStartEvent;

			StartOrientation[] startOrientations = possessionStartEvent.TargetPossessionObject.GetComponentsInChildren<StartOrientation>();

			foreach (StartOrientation startOrientation in startOrientations)
				startOrientation.ResetOrientation();
		}
	}
}