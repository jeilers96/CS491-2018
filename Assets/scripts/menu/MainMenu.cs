using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject MainMenuUI;

	public void ContinueGame(){
		SceneManager.LoadScene ("Continue Game");
	}

	public void StartNewGame(){
		SceneManager.LoadScene ("Level 1");
	}

	public void QuitGame(){
		Debug.Log ("Quitting game...");
		Application.Quit ();
	}
}
