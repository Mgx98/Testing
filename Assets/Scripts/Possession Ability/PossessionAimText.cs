using UnityEngine;
using PossessionAbility.Events;
using CM.Events;

namespace PossessionAbility
{
	public class PossessionAimText : MonoBehaviour
	{
		public GameObject textObject;
		public Vector3 offset;
		public bool invertForwardPosition = true;

		private Camera _camera;

		private void Awake()
		{
			EventManager.AddListener<AimAtPossessableEnterEvent>(OnAimAtPossessableEnter);
			EventManager.AddListener<AimAtPossessableExitEvent>(OnAimAtPossessableExit);

			_camera = Camera.main;

			if (!textObject.scene.IsValid())
				textObject = Instantiate(textObject);

			textObject.SetActive(false);
		}

		private void Update()
		{
			if (!textObject.activeSelf)
				return;

			textObject.transform.LookAt(_camera.transform.position);

			if (invertForwardPosition)
				textObject.transform.rotation = Quaternion.Euler(new Vector3(
					textObject.transform.eulerAngles.x,
					textObject.transform.eulerAngles.y + 180,
					textObject.transform.eulerAngles.z
				));
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

			textObject.transform.position = aimAtPossessableEnterEvent.TargetPossessionObject.transform.position + offset;

			textObject.SetActive(true);
		}

		private void OnAimAtPossessableExit(object eventData)
		{
			AimAtPossessableExitEvent aimAtPossessableExitEvent = eventData as AimAtPossessableExitEvent;

			textObject.transform.position = aimAtPossessableExitEvent.TargetPossessionObject.transform.position + offset;

			textObject.SetActive(false);
		}
	}
}