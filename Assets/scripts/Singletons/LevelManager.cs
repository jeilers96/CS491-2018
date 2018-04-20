using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class LevelManager : MonoBehaviour {

	//static instance for other scripts to reference
	public static LevelManager instance;
	public bool retrieveSaveData = true;
	public Transform playerOne;
	public Transform playerTwo;
	public List<GameObject> playerOneCharacters;
	public List<GameObject> playerTwoCharacters;
	public int spawnPointIndex = 0;
	public int PlayerOneIndex = 0;
	public int PlayerTwoIndex = 0;
	public SerialPort serial;
	public List<Vector3> spawnPoints = new List<Vector3>();

	private Transform SpawnPointsTransform;
	private SpawnPointManager spawnPointManager;
	private PlayerUIManager playerUIManager;

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
		SetCameraAndPlayerPositions (false);
		if (retrieveSaveData) {
			Load ();
		}
		
		if(Array.IndexOf(System.IO.Ports.SerialPort.GetPortNames(), "COM1") >= 0) {
			serial = new SerialPort("COM1", 9600);
		}
		
		if(serial != null) {
			if(!serial.IsOpen) {
				serial.Open();
			}
			serial.ReadTimeout = 1;
		}
	}

	void Start(){
		spawnPointManager = SpawnPointManager.instance;
		playerUIManager = PlayerUIManager.instance;
		if (retrieveSaveData) {
			spawnPointManager.Load ();
		}

		//SpawnPointsTransform = GameObject.Find ("SpawnPoints").transform;
		SetSpawnPointPositions ();
	}

	/// <summary>
	/// Swap out player one reference transform to the new character the player is switching to
	/// </summary>
	/// <param name="newCharacter">New character.</param>
	public void PlayerOneSwap(GameObject newCharacter){
		playerOne = newCharacter.transform;
		PlayerOneIndex++;
		PlayerOneIndex %= playerOneCharacters.Count;
		playerUIManager.UpdatePlayer1UI ();
	}

	/// <summary>
	/// Swap out player two reference transform to the new character the player is switching to
	/// </summary>
	/// <param name="newCharacter">New character.</param>
	public void PlayerTwoSwap(GameObject newCharacter){
		playerTwo = newCharacter.transform;
		PlayerTwoIndex++;
		PlayerTwoIndex %= playerTwoCharacters.Count;
		playerUIManager.UpdatePlayer2UI ();
	}

	public void RespawnPlayers(){
		Save ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

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
		SetCameraAndPlayerPositions (true);
	}

	void SetCameraAndPlayerPositions(bool shouldUseSpawnPoints){
		if(spawnPoints.Count > 0) {
			if (!shouldUseSpawnPoints) {
				Camera.main.transform.position = playerOne.position;
			} else {
				Camera.main.transform.position = spawnPoints [spawnPointIndex];
				playerOne.position = spawnPoints [spawnPointIndex];
				Vector3 playerTwoPosition = spawnPoints [spawnPointIndex];
				playerTwoPosition.x -= 2.5f;
				playerTwo.position = playerTwoPosition;
			}
		}
	}

	public void DeleteSaveData(){
		SaveLoadManager.DeleteLevelManagerSaveData ();
		SaveLoadManager.DeleteSpawnPointsManagerSaveData ();
		print ("Deleted: " + SaveLoadManager.LevelManagerFilePath + "\n"
			+ SaveLoadManager.SpawnPointManagerFilePath);
	}

	void SetSpawnPointPositions(){
		spawnPoints = new List<Vector3> ();
		if (SpawnPointsTransform != null) {
			for (int i = 0; i < SpawnPointsTransform.childCount; i++) {
				spawnPoints.Add (SpawnPointsTransform.GetChild (i).position);
			}
		} else {
			print ("SpawnPointsTransform is required by the level manager");
		}

	}

	public List<Vector3> GetSpawnPoints(){
		return spawnPoints;
	}
}