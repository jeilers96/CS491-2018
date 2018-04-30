using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gameController;
	
	public bool p1AtTeleporter = false;
	public bool p2AtTeleporter = false;

	void Awake(){
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != null) {
			Destroy (gameObject);
		}
	}
	
	void Update() {
		if (p1AtTeleporter && p2AtTeleporter) {
			LoadNextLevel();
			p1AtTeleporter = false;
			p2AtTeleporter = false;
		}
	}

	public void LoadNextLevel(){
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

		switch (sceneName){
		case "LoadingScreen":
			SceneManager.LoadScene("Tutorial 1");
			break;
		case "Tutorial 1":
			SceneManager.LoadScene("Tutorial 2");
			break;
		case "Tutorial 2":
			SceneManager.LoadScene("Level 1-1");
			break;
		case "Level 1-1":
			SceneManager.LoadScene("Level 1-2");
			break;
		case "Level 1-2":
			SceneManager.LoadScene("Tutorial 3");
			break;
		case "Tutorial 3":
			SceneManager.LoadScene("Level 2-1");
			break;
		default:
			SceneManager.LoadScene("LoadingScreen");
			break;
		}
	}
		
	// make main menu have just new game button
	//new game - erases all save data and loads first scene
}
