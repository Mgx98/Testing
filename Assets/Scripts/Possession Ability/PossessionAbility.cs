using CM.Events;
using PossessionAbility.Events;
using System.Linq;
using UnityEngine;

namespace PossessionAbility
{
	/// <summary>
	/// Can be used to possess objects through static methods
	/// </summary>
	public static class PossessionAbility
	{
		/// <summary>
		/// Try to possess the closest object from a specified location
		/// </summary>
		/// <param name="currentPossessionObject">The object to possess from</param>
		/// <param name="position">The position to possess from</param>
		/// <param name="range">The range used to check for possessable objects</param>
		/// <param name="possessableMask">The mask to possess</param>
		public static void TryPossessFrom(GameObject currentPossessionObject, Vector3 position, float range, LayerMask possessableMask)
		{
			Collider[] colliders = Physics.OverlapSphere(position, range, possessableMask);
			GameObject[] gameObjects = colliders.Select(collider => collider.gameObject).ToArray();

			float closestRange = -1;
			GameObject closestGameObject = GameObjectHelper.GetClosestGameObject(position, gameObjects, out closestRange);

			// Return if there is no object found in range
			if (closestRange == -1)
				return;

			Possess(currentPossessionObject, closestGameObject);
		}

		/// <summary>
		/// Possess any object
		/// </summary>
		/// <param name="currentPossessionObject">The object to possess from</param>
		/// <param name="targetPossessionObject">The object to possess</param>
		public static void Possess(GameObject currentPossessionObject, GameObject targetPossessionObject)
		{
			EventManager.Trigger(new PossessionStartEvent(currentPossessionObject, targetPossessionObject, 0));
		}

		/// <summary>
		/// Possess any object
		/// </summary>
		/// <param name="currentPossessionObject">The object to possess from</param>
		/// <param name="targetPossessionObject">The object to possess</param>
		/// <param name="delay">The delay before possessing the target object</param>
		public static void Possess(GameObject currentPossessionObject, GameObject targetPossessionObject, float delay)
		{
			EventManager.Trigger(new PossessionStartEvent(currentPossessionObject, targetPossessionObject, delay));
		}
	}
}