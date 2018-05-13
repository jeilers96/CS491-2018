using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour {

	public static SpawnPointManager instance;
	public bool[] spawnPointsActive;
	public int playersInSpawnPoint;

	private LevelManager levelManager;
	private int spawnPointsCount;

	void Awake(){
		instance = this;
		spawnPointsCount = transform.childCount;
		spawnPointsActive = new bool[spawnPointsCount]; 
	}

	void Start(){
		levelManager = LevelManager.instance;
		for(int i = 0; i < spawnPointsActive.Length; i++) {
			spawnPointsActive[i] = true;
		}

		if (levelManager.retrieveSaveData) {
			Load ();
		}
	}

	public void Save(){
		SaveLoadManager.SaveSpawnPointsManager (this);
	}

	public void Load(){
		bool[] tempSpawnPointsActive = SaveLoadManager.LoadSpawnPointsManager ();
		if (tempSpawnPointsActive != null) {
			spawnPointsActive = tempSpawnPointsActive;
			for (int i = 0; i < spawnPointsActive.Length; i++) {
				if (!spawnPointsActive [i]) {
					transform.GetChild (i).gameObject.SetActive (false);
				}
			}
		}
	}
}
