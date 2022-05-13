using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	public SpawnPrefab[] spawnablePrefabs;
	public float time;

	public static readonly int MaxSpawnables = 30;
	public static int currentSpawnablesCount = 0;

	private void Awake()
	{
		currentSpawnablesCount = 0;
	}

	private void Start()
	{
		StartCoroutine(SpawnRoutine(time));
	}

	private IEnumerator SpawnRoutine(float time)
	{
		while (true)
		{
			if (currentSpawnablesCount < MaxSpawnables)
			{
				GameObject gameObject = GetSpawnable();
				Instantiate(gameObject, transform.position, transform.rotation);

				currentSpawnablesCount++;
			}

			yield return new WaitForSeconds(time);
		}
	}

	private GameObject GetSpawnable()
    {
		GameObject spawnableObject = null;
		bool gotSpawnable = false;

		while (!gotSpawnable)
        {
			SpawnPrefab spawnablePrefab = spawnablePrefabs[Random.Range(0, spawnablePrefabs.Length)];

			if (Random.Range(0, 100) <= spawnablePrefab.chance)
            {
				spawnableObject = spawnablePrefab.prefab;
				gotSpawnable = true;
			}
		}

		return spawnableObject;
	}

	[System.Serializable]
	public struct SpawnPrefab
    {
		public GameObject prefab;
		public float chance;
    }
}