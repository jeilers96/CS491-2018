using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gameController;
	
	public bool p1AtTeleporter = false;
	public bool p2AtTeleporter = false;
	public int savedSceneIndex = 0;
	public int savedSpawnPointIndex = 0;
	public bool loadSaveData = false;

	private LevelManager levelManager;

	void Awake(){
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != null) {
			Destroy (gameObject);
		}
	}

	void Start(){
		levelManager = LevelManager.instance;
	}
	
	void Update() {
		if (p1AtTeleporter && p2AtTeleporter) {
			print ("Loading next level");
			LoadNextLevel();
			p1AtTeleporter = false;
			p2AtTeleporter = false;
		}
	}

	public void LoadNextLevel(){
		levelManager = LevelManager.instance;
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
		if (levelManager != null) {
			levelManager.DeleteSaveData ();
		} else {
			Debug.Log("level manager is null");
		}

		switch (sceneName){
		case "LoadingScreen":
			SaveGame(2);
			SceneManager.LoadScene("Tutorial 1");
			break;
		case "Tutorial 1":
			SaveGame (3);
			SceneManager.LoadScene("Tutorial 2");
			break;
		case "Tutorial 2":
			SaveGame (4);
			SceneManager.LoadScene("Level 1-1");
			break;
		case "Level 1-1":
			SaveGame (5);
			SceneManager.LoadScene("Level 1-2");
			break;
		case "Level 1-2":
			SaveGame (6);
			SceneManager.LoadScene("Tutorial 3");
			break;
		case "Tutorial 3":
			SaveGame (7);
			SceneManager.LoadScene("Level 2-1");
			break;
		default:
			SceneManager.LoadScene("MainMenu");
			break;
		}
	}

	public void SaveGame(int buildIndex, int spawnPointIndex = 0){
		print ("Saving game...");
		if (buildIndex > 1) { //loading screen in 0 and main menu is 1
			SaveLoadManager.SaveGameManager(buildIndex, spawnPointIndex);
			savedSceneIndex = buildIndex;
			savedSpawnPointIndex = spawnPointIndex;
			print ("Successfully saved");
		}
	}

	public void ContinueGame(){
		print ("Continuing the Game...");
		int[] saveData = new int[2];
		saveData = SaveLoadManager.LoadSaveGameManager ();
		if (saveData == null) {
			print ("No save data so start from level 1");
			savedSceneIndex = 2;
			savedSpawnPointIndex = 0;
		} else {
			savedSceneIndex = saveData [0];
			savedSpawnPointIndex = saveData [1];
		}
		loadSaveData = true;
		SceneManager.LoadScene (savedSceneIndex);
	}
}
