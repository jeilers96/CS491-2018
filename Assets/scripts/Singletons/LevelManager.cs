using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class LevelManager : MonoBehaviour {

	//static instance for other scripts to reference
	public static LevelManager instance;
	public static GameController gameController;
	public bool retrieveSaveData = true;
	public Transform playerOne;
	public Transform playerTwo;
	public List<GameObject> playerOneCharacters;
	public List<GameObject> playerTwoCharacters;
	public int spawnPointIndex = 0;
	public int PlayerOneIndex = 0;
	public int PlayerTwoIndex = 0;
	public SerialPort serial1;
	public SerialPort serial2;
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

	void Awake(){
		instance = this;
		if(Array.IndexOf(System.IO.Ports.SerialPort.GetPortNames(), "COM3") >= 0) {
			serial1 = new SerialPort("COM3", 9600);
			
		}
		
		if(Array.IndexOf(System.IO.Ports.SerialPort.GetPortNames(), "COM1") >= 0) {
			serial2 = new SerialPort("COM1", 9600);
			
		}
		
		/*if(serial1 != null) {
			if(!serial1.IsOpen) {
				serial1.Open();
			}
			serial1.ReadTimeout = 1;
		}
		
		if(serial2 != null) {
			if(!serial2.IsOpen) {
				serial2.Open();
			}
			serial2.ReadTimeout = 1;
		}*/
	}

	void Start(){
		if (serial1 != null) {
			serial1.Open();
		}
		
		if (serial2 != null) {
			serial2.Open();
		}
		
		if(serial1 != null && serial1.IsOpen) {
			 Debug.Log("Port 1 opened");
		}  else {
			Debug.Log("Could not open port 1");

		}
		
		if(serial2 != null && serial2.IsOpen) {
			 Debug.Log("Port 2 opened");
		}  else {
			Debug.Log("Could not open port 2");

		}

		spawnPointManager = SpawnPointManager.instance;
		playerUIManager = PlayerUIManager.instance;
		if (retrieveSaveData) {
			spawnPointManager.Load ();
		}

		if(GameObject.Find("SpawnPoints") != null) {
			SpawnPointsTransform = GameObject.Find("SpawnPoints").transform;
			SetSpawnPointPositions ();
		}

		SetCameraAndPlayerPositions ();
		if (retrieveSaveData) {
			Load ();
		}
		
		if(GameObject.Find("GameController") != null) {
			gameController = GameController.gameController;
		}
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
		if (spawnPointIndex < spawnPoints.Count) {
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
		print ("spawn point count " + spawnPoints.Count + " spawnPointIndex " + spawnPointIndex);
		if(spawnPoints.Count > 0 && spawnPointIndex > 0) {
			print ("setting pos to: " + spawnPoints [spawnPointIndex - 1]);
			Camera.main.transform.position = spawnPoints [spawnPointIndex - 1];
			playerOne.position = spawnPoints [spawnPointIndex - 1];
			Vector3 playerTwoPosition = spawnPoints [spawnPointIndex - 1];
			playerTwoPosition.x -= 2.5f;
			playerTwo.position = playerTwoPosition;
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
		} 
	}

	public List<Vector3> GetSpawnPoints(){
		return spawnPoints;
	}
	
	public void SetPlayerAtTeleporter(int playerNum, bool isAtTeleporter) {
		if(playerNum == 1) {
			gameController.p1AtTeleporter = isAtTeleporter;
		}
		else if(playerNum == 2) {
			gameController.p2AtTeleporter = isAtTeleporter;
		}
	}
	
	void OnDestroy() {
		if(serial1 != null) {
			serial1.Close();
		}
		if(serial2 != null) {
			serial2.Close();
		}
	}
}