using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour {

	public static SpawnPointManager instance;
	public bool[] spawnPointsActive;

	private LevelManager levelManager;

	void Awake(){
		instance = this;
	}

	void Start(){
		levelManager = LevelManager.instance;
		spawnPointsActive = new bool[transform.childCount]; 
		for(int i = 0; i < spawnPointsActive.Length; i++) {
			spawnPointsActive[i] = true;
		}

		//get all children and if their spawn point active is false, destroy the object or make them inactive

		if (levelManager.retrieveSaveData) {
			Load ();

			for (int i = 0; i < transform.childCount; i++) {
				if (!spawnPointsActive [i]) {
					transform.GetChild (i).gameObject.SetActive (false);
				}
			}
		}
	}

	public void Save(){
		SaveLoadManager.SaveSpawnPointsManager (this);
	}

	public void Load(){
		spawnPointsActive = SaveLoadManager.LoadSpawnPointsManager (this);
	}
}
