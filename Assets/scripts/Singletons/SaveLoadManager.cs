﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager{

	public static string LevelManagerFilePath = Application.persistentDataPath + "/levelManager.dat";
	public static string SpawnPointManagerFilePath = Application.persistentDataPath + "/spawnPointManager.dat";

	/**** Level Manager Methods ****/

	public static void SaveLevelManager(LevelManager levelManager){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fStream = new FileStream(LevelManagerFilePath, FileMode.Create);

		LevelManagerData levelManagerData = new LevelManagerData (levelManager);

		bf.Serialize (fStream, levelManagerData);
		fStream.Close ();
	}

	public static float[] LoadLevelManager(ref int spawnPointIndex){
		if (File.Exists (LevelManagerFilePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fStream = new FileStream (LevelManagerFilePath, FileMode.Open);

			LevelManagerData characterData = bf.Deserialize (fStream) as LevelManagerData;
			fStream.Close ();

			spawnPointIndex = characterData.spawnPointIndex;
			return characterData.playerSpawnPositions;
		} else {
			return new float[6];
		}
	}

	public static void DeleteLevelManagerSaveData(){
		if (File.Exists (LevelManagerFilePath)) {
			File.Delete (LevelManagerFilePath);
		}
	}

	/**** Spawn Points Manager Methods ****/

	public static void SaveSpawnPointsManager(SpawnPointManager spawnPointManager){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fStream = new FileStream(SpawnPointManagerFilePath, FileMode.Create);

		SpawnPointManagerData spawnPointManagerData = new SpawnPointManagerData (spawnPointManager);

		bf.Serialize (fStream, spawnPointManagerData);
		fStream.Close ();
	}

	public static bool[] LoadSpawnPointsManager(SpawnPointManager spawnPointManager){
		if (File.Exists (SpawnPointManagerFilePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fStream = new FileStream (SpawnPointManagerFilePath, FileMode.Open);

			SpawnPointManagerData spawnPointManagerData = bf.Deserialize (fStream) as SpawnPointManagerData;
			fStream.Close ();

			return spawnPointManagerData.spawnPointsActive;
		} else {
			bool[] defaultSpawnPoints = new bool[spawnPointManager.spawnPointsActive.Length];
			for (int i = 0; i < defaultSpawnPoints.Length; i++) {
				defaultSpawnPoints [i] = true;
			}

			return defaultSpawnPoints;
		}
	}

	public static void DeleteSpawnPointsManagerSaveData(){
		if (File.Exists (SpawnPointManagerFilePath)) {
			File.Delete (SpawnPointManagerFilePath);
		}
	}


}

[Serializable]
public class LevelManagerData {
	public float[] playerSpawnPositions;
	public int spawnPointIndex;
	public LevelManagerData(LevelManager levelManager){
		playerSpawnPositions = new float[6];
		spawnPointIndex = levelManager.spawnPointIndex;
//		playerSpawnPositions [0] = levelManager.playerOne.position.x;
//		playerSpawnPositions [1] = levelManager.playerOne.position.y;
//		playerSpawnPositions [2] = levelManager.playerOne.position.z;
//		playerSpawnPositions [3] = levelManager.playerTwo.position.x;
//		playerSpawnPositions [4] = levelManager.playerTwo.position.y;
//		playerSpawnPositions [5] = levelManager.playerTwo.position.z;
	}
}

[Serializable]
public class SpawnPointManagerData {
	public bool[] spawnPointsActive;
	public SpawnPointManagerData(SpawnPointManager spawnPointManager){
		spawnPointsActive = new bool[spawnPointManager.spawnPointsActive.Length];
		for(int i = 0; i < spawnPointsActive.Length; i++){
			spawnPointsActive [i] = spawnPointManager.spawnPointsActive [i];
		}
	}
}