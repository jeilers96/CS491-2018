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
