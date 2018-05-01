using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	private bool loadScene = false;

	[SerializeField]
	private int scene;
	[SerializeField]
	private Text loadingText;
	
	private String nextScene;
	
	void Start() {
		scene = GetNextSceneName();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space) && !loadScene) {
			loadScene = true;

			loadingText.text = "Loading...";
			StartCoroutine (LoadNewScene ());
		}
			
		// pulse the transparency of the loading text.
		loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
	}

	IEnumerator LoadNewScene() {
		// This line is only necessary because our scenes are so simple that they load too fast to read the "Loading..." text.
		yield return new WaitForSeconds(3);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		SaveLoadManager.DeleteSpawnPointsManagerSaveData();
		SaveLoadManager.DeleteLevelManagerSaveData ();
		AsyncOperation async = SceneManager.LoadSceneAsync(scene);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		// while (!async.isDone) {
			// yield return null;
		// }
	}
	
	int GetNextSceneName() {
		Scene scene = SceneManager.GetActiveScene();
		int nextScene;
		
		switch(scene.name) {
			case "LoadingScreen":
				nextScene = 1;
				break;
			default:
				nextScene = 0;
				break;
		}
		
		return nextScene;
	}
}
