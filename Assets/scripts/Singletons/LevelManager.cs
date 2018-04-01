using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	//static instance for other scripts to reference
	public static LevelManager instance;
	public bool retrieveSaveData = true;
	public Transform playerOne;
	public Transform playerTwo;
	public List<GameObject> playerOneCharacters;
	public List<GameObject> playerTwoCharacters;
	public List<Vector3> spawnPoints;
	public int spawnPointIndex = 0;

	private SpawnPointManager spawnPointManager;

	/// <summary>
	/// The player1 key codes in order of move left, move right, jump, swap, ability.
	/// </summary>
	public List<KeyCode> player1KeyCodes = new List<KeyCode>(){
		KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.E, KeyCode.R
	};

	/// <summary>
	/// The player2 key codes in order of move left, move right, jump, swap, ability.
	/// </summary>
	public List<KeyCode> player2KeyCodes = new List<KeyCode>(){
		KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.K, KeyCode.L
	};

	private float pauseGameSpeed = 0.0f;
	private float regularGameSpeed = 1.0f;

	void Awake(){
		instance = this;
		SetCameraAndPlayerPositions ();
		if (retrieveSaveData) {
			Load ();
		}
	}

	void Start(){
		spawnPointManager = SpawnPointManager.instance;
		if (retrieveSaveData) {
			spawnPointManager.Load ();
		}
	}

	/// <summary>
	/// Swap out player one reference transform to the new character the player is switching to
	/// </summary>
	/// <param name="newCharacter">New character.</param>
	public void PlayerOneSwap(GameObject newCharacter){
		playerOne = newCharacter.transform;
	}

	/// <summary>
	/// Swap out player two reference transform to the new character the player is switching to
	/// </summary>
	/// <param name="newCharacter">New character.</param>
	public void PlayerTwoSwap(GameObject newCharacter){
		playerTwo = newCharacter.transform;
	}

	public void RespawnPlayers(){
		Save ();
		SceneManager.LoadScene ("double jump man's world");

	}

	public void SetNewSpawnPoint(){
		if (spawnPointIndex != spawnPoints.Count - 1) {
			spawnPointManager.spawnPointsActive [spawnPointIndex] = false;
			spawnPointIndex++;
		}
	}

	public void Save(){
		SaveLoadManager.SaveLevelManager (this);
		spawnPointManager.Save ();
	}

	public void Load(){
		SaveLoadManager.LoadLevelManager (ref spawnPointIndex);
		SetCameraAndPlayerPositions ();
	}

	void SetCameraAndPlayerPositions(){
		Camera.main.transform.position = spawnPoints [spawnPointIndex];
		playerOne.position = spawnPoints [spawnPointIndex];
		Vector3 playerTwoPosition = spawnPoints [spawnPointIndex];
		playerTwoPosition.x -= 2.5f;
		playerTwo.position = playerTwoPosition;
	}

	public void DeleteSaveData(){
		SaveLoadManager.DeleteLevelManagerSaveData ();
		SaveLoadManager.DeleteSpawnPointsManagerSaveData ();
		print ("Deleted: " + SaveLoadManager.LevelManagerFilePath + "\n"
			+ SaveLoadManager.SpawnPointManagerFilePath);
	}
}