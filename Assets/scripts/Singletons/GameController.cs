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
		case "speed_boost":
			SceneManager.LoadScene("1-Tutorial");
			break;
		case "1-Tutorial":
			SceneManager.LoadScene("2-Tutorial");
			break;
		case "2-Tutorial":
			SceneManager.LoadScene("Level 1-1");
			break;
		case "Level 1-1":
			break;
		default:
			break;
		}
	}
}
