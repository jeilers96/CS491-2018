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

	/// <summary>
	/// The player1 key codes in order of move left, move right, jump, swap, ability.
	/// </summary>
	public List<KeyCode> player1KeyCodes = new List<KeyCode>()
	{
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

	void PlayerOneSwap(GameObject newCharacter){
		playerOne = newCharacter.transform;
	}

	void PlayerTwoSwap(GameObject newCharacter){
		playerTwo = newCharacter.transform;
	}
}
