using CM.Events;
using PossessionAbility.Events;
using System.Collections;
using UnityEngine;

namespace PossessionAbility
{
	/// <summary>
	/// A visual effect that plays while possessing an object
	/// </summary>
	public class PossessionEffect : MonoBehaviour
	{
		public float ForceFieldDelay
		{
			get
			{
				return _possessionDelay / 100 * forceFieldDelayPercentage;
			}
		}

		[Range(0.0f, 100.0f)]
		public float forceFieldDelayPercentage = 40f;
		public Vector3 offset;

		[SerializeField]
		private ParticleSystem _particleSystem;

		[SerializeField]
		private ParticleSystemForceField _particleSystemForceField;

		[SerializeField]
		private bool _spawnFromCurrentPossessable = true;

		[SerializeField]
		private Transform _spawnTransform;

		[SerializeField]
		private AudioSource _possessionSound;

		private ParticleSystemForceField _particleSystemForceFieldInstance;

		private float _possessionDelay;

		private void Awake()
		{
			EventManager.AddListener<PossessionStartEvent>(OnPossessionStart);
			EventManager.AddListener<PossessionSwapEvent>(OnPossessionSwap);
		}

		private void OnDestroy()
		{
			EventManager.RemoveListener<PossessionStartEvent>(OnPossessionStart);
			EventManager.RemoveListener<PossessionSwapEvent>(OnPossessionSwap);
		}

		private void OnPossessionStart(object eventData)
		{
			PossessionStartEvent possessionStartEvent = eventData as PossessionStartEvent;

			_possessionSound.Play();

			_possessionDelay = possessionStartEvent.Delay;

			if (_spawnFromCurrentPossessable)
				_spawnTransform = possessionStartEvent.CurrentPossessionObject.transform;

			ParticleSystem particleSystem = Instantiate(
				_particleSystem,
				_spawnTransform.position + _spawnTransform.forward * offset.x + _spawnTransform.up * offset.y + _spawnTransform.right * offset.z,
				_spawnTransform.rotation
			);

			ParticleSystem.MainModule mainModule = particleSystem.main;
			mainModule.startLifetime = possessionStartEvent.Delay;

			StartCoroutine(InstantiateForceFieldRoutine(ForceFieldDelay, possessionStartEvent.TargetPossessionObject.transform.position));
		}

		private void OnPossessionSwap(object eventData)
		{
			StopAllCoroutines();

			if (!_particleSystemForceFieldInstance)
				return;

			Destroy(_particleSystemForceFieldInstance.gameObject);
		}

		private IEnumerator InstantiateForceFieldRoutine(float delay, Vector3 position)
		{
			yield return new WaitForSeconds(delay);

			_particleSystemForceFieldInstance = Instantiate(_particleSystemForceField, position, Quaternion.identity);
		}
	}
}