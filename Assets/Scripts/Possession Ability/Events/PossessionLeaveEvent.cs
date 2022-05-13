using System;
using UnityEngine;

namespace PossessionAbility.Events
{
	/// <summary>
	/// An event that triggers when the default ghost is going to be possessed
	/// </summary>
	public class PossessionLeaveEvent : EventArgs
	{
		public GameObject CurrentPossessionObject { get; private set; }
		public float Delay { get; private set; }

		public PossessionLeaveEvent(GameObject currentPossessionObject, float delay)
		{
			CurrentPossessionObject = currentPossessionObject;
			Delay = delay;
		}
	}
}
