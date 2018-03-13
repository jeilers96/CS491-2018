using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	//static instance for other scripts to reference
	public static LevelManager instance;
	public Transform playerOne;
	public Transform playerTwo;
	public List<GameObject> playerOneCharacters;
	public List<GameObject> playerTwoCharacters;
	public List<Vector3> spawnPoints; //player2.x = player1.x - 2.5f
	public int spawnPointIndex = 0;

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
		Camera.main.transform.position = spawnPoints [spawnPointIndex];
		playerOne.position = spawnPoints [spawnPointIndex];
		Vector3 playerTwoPosition = spawnPoints [spawnPointIndex];
		playerTwoPosition.x -= 2.5f;
		playerTwo.position = playerTwoPosition;
	}

	public void SetNewSpawnPoint(){
		if (spawnPointIndex != spawnPoints.Count - 1) {
			spawnPointIndex++;
		}
	}

	//setup a method here to change spawn point of players on start or restart of level,
	//and make an inspector class so this can be updated using a button in inspector
}
