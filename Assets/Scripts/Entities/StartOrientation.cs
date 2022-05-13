using UnityEngine;

public class StartOrientation : MonoBehaviour
{
	private Vector3 _startPosition;
	private Quaternion _startRotation;

	public void ResetOrientation()
	{
		Destroy(GetComponent<Rigidbody>());

		transform.localPosition = _startPosition;
		transform.localRotation = _startRotation;
	}

	private void Awake()
	{
		_startPosition = transform.localPosition;
		_startRotation = transform.localRotation;
	}
}