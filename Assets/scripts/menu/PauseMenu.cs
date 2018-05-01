using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject PauseMenuUI;

	private LevelManager levelManager;
	private GameObject selectedButton;

	void Awake(){
		Time.timeScale = 1.0f;
	}

	void Start(){
		levelManager = LevelManager.instance;
		Transform pauseMenu = gameObject.transform.GetChild (0);
		if (pauseMenu != null && pauseMenu.transform.GetChild (0) != null) {
			selectedButton = pauseMenu.transform.GetChild (0).gameObject;
		}

		if (selectedButton != null) {
			Button button = selectedButton.GetComponent<Button> ();
			if (button != null) {
				button.Select ();
				button.OnSelect (null);
			}
		}
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

	public void Restart(){
		levelManager.DeleteSaveData ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void Respawn(){
		levelManager.RespawnPlayers ();
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void QuitGame(){
		Debug.Log ("Quitting game...");
		Application.Quit ();
	}
}
