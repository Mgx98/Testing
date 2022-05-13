using System;
using UnityEngine;

namespace PossessionAbility.Events
{
	/// <summary>
	/// An event that triggers when an object is being possessed
	/// </summary>
	public class PossessionSwapEvent : EventArgs
	{
		public GameObject CurrentPossessionObject { get; private set; }
		public GameObject TargetPossessionObject { get; private set; }

		public PossessionSwapEvent(GameObject currentPossessionObject, GameObject targetPossessionObject)
		{
			CurrentPossessionObject = currentPossessionObject;
			TargetPossessionObject = targetPossessionObject;
		}
	}
}
