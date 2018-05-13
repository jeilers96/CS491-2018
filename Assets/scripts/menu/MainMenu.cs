using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject MainMenuUI;

	public void StartNewGame(){
		Debug.Log ("Deleting all save data");
		SaveLoadManager.DeleteSpawnPointsManagerSaveData();
		SaveLoadManager.DeleteLevelManagerSaveData ();
		SaveLoadManager.DeleteSaveGameManagerData ();

		SceneManager.LoadScene ("Tutorial 1");
	}

	public void ContinueGame(){
		Debug.Log ("Loading save...");
		GameController.gameController.ContinueGame ();
	}

	public void QuitGame(){
		Debug.Log ("Quitting game...");
		Application.Quit ();
	}
}
