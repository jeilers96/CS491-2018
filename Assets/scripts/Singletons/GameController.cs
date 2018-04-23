using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gameController;

	void Awake(){
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != null) {
			Destroy (gameObject);
		}
	}

	public void LoadNextLevel(){
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

		switch (sceneName){
		case "1-1":
			SceneManager.LoadScene ("1-2");
			break;
		}
	}
}
