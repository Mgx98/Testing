using System;
using UnityEngine;

namespace PossessionAbility.Events
{
	/// <summary>
	/// An event that triggers when you stop aiming at a possessable object
	/// </summary>
	public class AimAtPossessableExitEvent : EventArgs
	{
		public GameObject CurrentPossessionObject { get; private set; }
		public GameObject TargetPossessionObject { get; private set; }

		public AimAtPossessableExitEvent(GameObject currentPossessionObject, GameObject targetPossessionObject)
		{
			CurrentPossessionObject = currentPossessionObject;
			TargetPossessionObject = targetPossessionObject;
		}
	}
}
