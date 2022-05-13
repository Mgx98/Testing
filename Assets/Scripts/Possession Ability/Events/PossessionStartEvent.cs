using System;
using UnityEngine;

namespace PossessionAbility.Events
{
	/// <summary>
	/// An event that triggers when an object is going to be possessed
	/// </summary>
	public class PossessionStartEvent : EventArgs
	{
		public GameObject CurrentPossessionObject { get; private set; }
		public GameObject TargetPossessionObject { get; private set; }
		public float Delay { get; private set; }

		public PossessionStartEvent(GameObject currentPossessionObject, GameObject targetPossessionObject, float delay)
		{
			CurrentPossessionObject = currentPossessionObject;
			TargetPossessionObject = targetPossessionObject;
			Delay = delay;
		}
	}
}
