using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void StartNewGame(){
		SceneManager.LoadScene ("test_scene");
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void ContinueGame(){
		//load level select scene
	}
}
