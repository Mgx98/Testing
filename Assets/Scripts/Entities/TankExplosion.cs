using UnityEngine;
using UnityEngine.Events;

public class TankExplosion : MonoBehaviour
{
	public float explosionForce = 3;
	public float explosionRadius = 10;
	public float explosionUpwardsModifier = 1;
	public Vector3 explosionOffset;

	[SerializeField]
	private UnityEvent _explosionEvent;

	[SerializeField]
	private GameObject[] _objectsToExplode;

	public void Explode()
	{
		foreach (GameObject gameObject in _objectsToExplode)
		{
			Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();

			rigidbody.AddExplosionForce(
				explosionForce,
				transform.position + explosionOffset,
				explosionRadius,
				explosionUpwardsModifier,
				ForceMode.Impulse
			);
		}

		gameObject.layer = LayerMask.NameToLayer("Possessable");

		_explosionEvent?.Invoke();
	}
}