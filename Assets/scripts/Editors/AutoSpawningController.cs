using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawning controller, attached to each spawn point in order for it to spawn objects according to its variables.
/// </summary>
public class AutoSpawningController : MonoBehaviour {

	/// <summary>
	/// Should this spawn point infinitely create the objects specified in generatedObjects
	/// </summary>
	public bool infiniteCreate = false;
	/// <summary>
	/// The time between spawns of the objects
	/// </summary>
	public float spawnTime = 3.0f;
	/// <summary>
	/// The spawn point of these game objects.
	/// </summary>
	public GameObject spawnPoint;
	/// <summary>
	/// The number of objects to spawn, will be ignored if infinite create is set to true.
	/// </summary>
	public int numberOfObjects = 5;
	/// <summary>
	/// All the possible objects to spawn
	/// </summary>
	public List<GameObject> possibleObjects;
	/// <summary>
	/// The randomly ordered, generated objects to spawn.
	/// </summary>
	public List<GameObject> generatedObjects;

	private float timeCount = 0;
	private int objectCount = 0;

	void Start(){
		objectCount = generatedObjects.Count;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (objectCount > 0) {
			timeCount += Time.deltaTime;

			if (timeCount > spawnTime && objectCount > 0) {
				//instantiate the new object from the list and set its position to the object this script is attached to
				GameObject gObject = Instantiate (generatedObjects [objectCount - 1]);
				gObject.transform.SetParent (spawnPoint.transform, false);
				gObject.transform.localPosition = gameObject.transform.localPosition;

				objectCount--;
				timeCount = 0;

				//loop through all objects again and again if infinite create is on
				if (objectCount == 0 && infiniteCreate) {
					objectCount += generatedObjects.Count;
				}
			}
		}
	}

	/// <summary>
	/// Auto generates the amount of GameObjects specified by objectList
	/// </summary>
	public void AutoGenerate(){
		for (int z = 0; z < numberOfObjects; z++) {
			//pick a random object from all the possible objects
			int randomIndex = UnityEngine.Random.Range (0, possibleObjects.Count);
			generatedObjects.Add(possibleObjects [randomIndex]);
		}
	}
}

