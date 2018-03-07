using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawning controller, attached to each spawn point in order for it to spawn objects according to its variables.
/// </summary>
public class SpawningController : MonoBehaviour {

	/// <summary>
	/// Should this spawn point infinitely create objects
	/// </summary>
	public bool infiniteCreate = false;
	/// <summary>
	/// The time between spawns of the objects
	/// </summary>
	public float spawnTime = 3.0f;
	public GameObject spawnPoint;
	/// <summary>
	/// All the objects to spawn
	/// </summary>
	public List<GameObject> objectList;

	private int objectCount;
	private float timeCount = 0;

	void Start(){
		objectCount = objectList.Count;
	}

	// Update is called once per frame
	void FixedUpdate () {
		timeCount += Time.deltaTime;
		if (timeCount > spawnTime && objectCount > 0) {
			//instantiate the new object from the list and set its position to the object this script is attached to
			GameObject gObject = Instantiate (objectList[objectCount - 1]);
			gObject.transform.SetParent (spawnPoint.transform, false);
			gObject.transform.localPosition = gameObject.transform.localPosition;

			objectCount--;
			timeCount = 0;

			//if infinite create is on, loop through all objects again and again
			if (objectCount == 0 && infiniteCreate) {
				objectCount += objectList.Count;
			}
		}
	}
}

