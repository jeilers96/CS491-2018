using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject PauseMenuUI;

	private LevelManager levelManager;

	void Start(){
		levelManager = LevelManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume ();
			} else {
				Pause();
			}
		}
	}

	public void Resume(){
		if (PauseMenuUI != null) {
			PauseMenuUI.SetActive (!GameIsPaused);
		} else {
			Debug.Log ("Missing PauseMenuUI");
		}

		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause(){
		if (PauseMenuUI != null) {
			PauseMenuUI.SetActive (!GameIsPaused);
		} else {
			print ("Missing PauseMenuUI");
		}

		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Save(){
		levelManager.Save ();
	}

	public void LoadMenu(){
		SceneManager.LoadScene ("Menu");
	}

	public void QuitGame(){
		Debug.Log ("Quitting game...");
		Application.Quit ();
	}
}
