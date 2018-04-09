using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO.Ports;

public abstract class ingameCharacter : MonoBehaviour {
	public float moveSpeed;
	public float jumpForce;
	public Transform groundCheck;
	public LayerMask groundEntity;
	
	protected float movementDirection;
	protected bool facingRight = true;
	protected bool isGrounded = false;
	protected float groundRadius = 0.1f;
	
	protected LevelManager levelManager;
	protected int playerNum;
	protected int characterNum;
	protected Object otherCharacter;
	protected KeyCode keyLeft;
	protected KeyCode keyRight;
	protected KeyCode keyJump;
	protected KeyCode keyAction;
	protected KeyCode keySwap;
	
	protected const float DEFAULT_MOVE_SPEED = 8.0f;
	protected const float DEFAULT_JUMP_FORCE = 250.0f;
	
	private LevelManager manager;
	// Use this for initialization
	protected void Start () {
		Rigidbody2D rigid2D = GetComponent<Rigidbody2D> ();
		moveSpeed = DEFAULT_MOVE_SPEED;
		jumpForce = DEFAULT_JUMP_FORCE;
		rigid2D.freezeRotation = true;
		levelManager = LevelManager.instance;
		readyPlayer(levelManager);
	}
	
	// Update is called once per frame
	protected void FixedUpdate () {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundEntity);
	}
	
	protected void playerMove(Rigidbody2D rigidBody) {
		if(Input.GetKey(keyRight)) {
			movementDirection = 1;
		} else if(Input.GetKey(keyLeft)) {
			movementDirection = -1;
		} else {
			movementDirection = 0;
		}
		if(levelManager.serial.BytesToRead > 0 && (levelManager.serial.ReadByte() & (1 << 0)) == 1) {
			 movementDirection = 1;
		} else if(levelManager.serial.BytesToRead > 0 && (levelManager.serial.ReadByte() & (1 << 1)) == 1) {
			 movementDirection = -1;
		}
		
		rigidBody.velocity = new Vector2(movementDirection * moveSpeed, rigidBody.velocity.y);
		
		if(Mathf.Abs(movementDirection) > 0 && isGrounded) {
			GetComponent<Animator> ().SetBool ("walking",true);
			GetComponent<Animator> ().SetBool ("jumping",false);
		} else if(!isGrounded){
			GetComponent<Animator> ().SetBool ("walking",false);
			GetComponent<Animator> ().SetBool ("jumping",true);
		} else{
			GetComponent<Animator> ().SetBool ("walking",false);
			GetComponent<Animator> ().SetBool ("jumping",false);
		}
		
		if(movementDirection > 0 && !facingRight) {
			flip();
		} else if(movementDirection < 0 && facingRight){
			flip();
		}
	}
	
	protected void playerJump(Rigidbody2D rigidBody) {
		rigidBody.AddForce(new Vector2(0, jumpForce));
	}
	
	public abstract void playerAction(Rigidbody2D rigidBody);
	
	public abstract void resetPlayerState();
	
	protected void swapCharacter() {
		print ("Player num = " + playerNum);
		Vector2 characterPosition = new Vector2(transform.position.x, transform.position.y);
		GameObject newCharacter = GameObject.Instantiate(otherCharacter, new Vector2(characterPosition.x, characterPosition.y), Quaternion.identity) as GameObject;
		if (playerNum == 1) {
			print ("Player 1 swapping to: " + newCharacter.name);
			levelManager.PlayerOneSwap (newCharacter);
		} else if (playerNum == 2) {
			print ("Player 2 swapping to: " + newCharacter.name);
			levelManager.PlayerTwoSwap (newCharacter);
		}

		Destroy(gameObject);
	}
	
	protected void readyPlayer(LevelManager levelManager) {
		for(int i = 0; i < levelManager.playerOneCharacters.Count; i++) {
			if(levelManager.playerOneCharacters[i].name.Equals(gameObject.name)) {
				playerNum = 1;
				characterNum = i;
				break;
			} else if(levelManager.playerTwoCharacters[i].name.Equals(gameObject.name)){
				playerNum = 2;
				characterNum = i;
				break;
			}
		}

		setNextCharacter(playerNum);
		setKeyCodes(playerNum);
	}
	
	protected void setNextCharacter(int playerNum) {
		if(playerNum == 1) {
			int index = (characterNum + 1) % levelManager.playerOneCharacters.Count;
			otherCharacter = levelManager.playerOneCharacters[index];
		} else if(playerNum == 2) {
			int index = (characterNum + 1) % levelManager.playerTwoCharacters.Count;
			otherCharacter = levelManager.playerTwoCharacters[index];
		}
	}
	
	protected void setKeyCodes(int playerNum) {
		if(playerNum == 1) {
			keyLeft = levelManager.player1KeyCodes[0];
			keyRight = levelManager.player1KeyCodes[1];
			keyJump = levelManager.player1KeyCodes[2];
			keySwap = levelManager.player1KeyCodes[3];
			keyAction = levelManager.player1KeyCodes[4];
		} else if (playerNum == 2) {
			keyLeft = levelManager.player2KeyCodes[0];
			keyRight = levelManager.player2KeyCodes[1];
			keyJump = levelManager.player2KeyCodes[2];
			keySwap = levelManager.player2KeyCodes[3];
			keyAction = levelManager.player2KeyCodes[4];
		}
	}
	
	protected void flip() {
		facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
	}
	
	protected void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name == "water" && this.gameObject.tag != "swimmer") {
			//GetComponent<SpriteRenderer>().enabled = false;
			//Application.LoadLevel ("double jump man's world");
			levelManager.RespawnPlayers();
		}
		 
		if(other.gameObject.name == "laserBullet") {
			GetComponent<SpriteRenderer>().enabled = false;
			//Application.LoadLevel("speed_boost");
			levelManager.RespawnPlayers();
		}
		
		if(other.gameObject.name == "spikes") {
			GetComponent<SpriteRenderer>().enabled = false;
			//Application.LoadLevel("speed_boost");
			levelManager.RespawnPlayers();
		}
	}

	protected void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "SpawnPoint") {
			levelManager.SetNewSpawnPoint ();
			Destroy (other.gameObject);
		}
	}
}
