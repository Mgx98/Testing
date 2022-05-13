using UnityEngine;
using PossessionAbility.Events;
using CM.Events;

namespace PossessionAbility
{
	public class PossessionAimEffect : MonoBehaviour
	{
		public Color color;

		[Range(0f, 1f)]
		public float intensity = 0.5f;

		private void Awake()
		{
			EventManager.AddListener<AimAtPossessableEnterEvent>(OnAimAtPossessableEnter);
			EventManager.AddListener<AimAtPossessableExitEvent>(OnAimAtPossessableExit);
		}

		private void OnDestroy()
		{
			EventManager.RemoveListener<AimAtPossessableEnterEvent>(OnAimAtPossessableEnter);
			EventManager.RemoveListener<AimAtPossessableExitEvent>(OnAimAtPossessableExit);
		}

		private void OnAimAtPossessableEnter(object eventData)
		{
			AimAtPossessableEnterEvent aimAtPossessableEnterEvent = eventData as AimAtPossessableEnterEvent;

			if (aimAtPossessableEnterEvent.CurrentPossessionObject.tag != "Ghost")
				return;

			MeshRenderer[] meshRenderers = aimAtPossessableEnterEvent.TargetPossessionObject.GetComponentsInChildren<MeshRenderer>();

			foreach (MeshRenderer meshRenderer in meshRenderers)
			{
				meshRenderer.material.color = color * intensity;
			}
		}

		private void OnAimAtPossessableExit(object eventData)
		{
			AimAtPossessableExitEvent aimAtPossessableExitEvent = eventData as AimAtPossessableExitEvent;

			MeshRenderer[] meshRenderers = aimAtPossessableExitEvent.TargetPossessionObject.GetComponentsInChildren<MeshRenderer>();

			foreach (MeshRenderer meshRenderer in meshRenderers)
			{
				meshRenderer.material.color = Color.white;
			}
		}
	}
}