using UnityEngine;

public static class GameObjectHelper
{
	public static GameObject GetClosestGameObject(Vector3 position, GameObject[] gameObjects, out float range)
	{
		GameObject closestGameObject = null;
		float closestRange = -1;

		foreach (GameObject gameObject in gameObjects)
		{
			float distance = Vector3.Distance(position, gameObject.transform.position);

			if ((!closestGameObject) || (distance < closestRange))
			{
				closestRange = distance;
				closestGameObject = gameObject;
			}
		}

		range = closestRange;

		return closestGameObject;
	}

	public static GameObject GetClosestGameObject(Vector2 position, GameObject[] gameObjects, out float range)
	{
		GameObject closestGameObject = null;
		float closestRange = -1;

		foreach (GameObject gameObject in gameObjects)
		{
			float distance = Vector2.Distance(position, gameObject.transform.position);

			if ((!closestGameObject) || (distance < closestRange))
			{
				closestRange = distance;
				closestGameObject = gameObject;
			}
		}

		range = closestRange;

		return closestGameObject;
	}
}