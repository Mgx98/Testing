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

		private void OnAimAtPossessableEnter(object eventData)
		{
			AimAtPossessableEnterEvent aimAtPossessableEnterEvent = eventData as AimAtPossessableEnterEvent;

			MeshRenderer meshRenderer = aimAtPossessableEnterEvent.TargetPossessionObject.GetComponent<MeshRenderer>();

			meshRenderer.material.SetColor("_EmissionColor", color * intensity);
			meshRenderer.material.EnableKeyword("_EMISSION");
		}

		private void OnAimAtPossessableExit(object eventData)
		{
			AimAtPossessableExitEvent aimAtPossessableExitEvent = eventData as AimAtPossessableExitEvent;

			MeshRenderer meshRenderer = aimAtPossessableExitEvent.TargetPossessionObject.GetComponent<MeshRenderer>();

			meshRenderer.material.SetColor("_EmissionColor", Color.black);
			meshRenderer.material.DisableKeyword("_EMISSION");
		}
	}
}