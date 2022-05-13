using System;
using UnityEngine;

namespace PossessionAbility.Events
{
	/// <summary>
	/// An event that triggers when you start aiming at a possessable object
	/// </summary>
	public class AimAtPossessableEnterEvent : EventArgs
	{
		public GameObject CurrentPossessionObject { get; private set; }
		public GameObject TargetPossessionObject { get; private set; }

		public AimAtPossessableEnterEvent(GameObject currentPossessionObject, GameObject targetPossessionObject)
		{
			CurrentPossessionObject = currentPossessionObject;
			TargetPossessionObject = targetPossessionObject;
		}
	}
}
