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
	
	protected bool isAtTeleporter = false;
	
	protected LevelManager levelManager;
	protected int playerNum;
	protected int characterNum;
	protected Object otherCharacter;
	protected KeyCode keyLeft;
	protected KeyCode keyRight;
	protected KeyCode keyJump;
	protected KeyCode keyAction;
	protected KeyCode keySwap;
	protected SerialPort serial;
	protected int byteRead;
	
	protected const float DEFAULT_MOVE_SPEED = 8.0f;
	protected const float DEFAULT_JUMP_FORCE = 250.0f;
	
	private LevelManager manager;
	private SpawnPointManager SPManager;

	// Use this for initialization
	protected void Start () {
		Rigidbody2D rigid2D = GetComponent<Rigidbody2D> ();
		moveSpeed = DEFAULT_MOVE_SPEED;
		jumpForce = DEFAULT_JUMP_FORCE;
		rigid2D.freezeRotation = true;
		levelManager = LevelManager.instance;
		SPManager = SpawnPointManager.instance;
		readyPlayer(levelManager);
	}
	
	protected void grounded() {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundEntity);
	}
	
	protected void getBytesFromInput(){
		if(serial.BytesToRead > 0) {
			byteRead = serial.ReadByte();
		} 
	}
	protected void playerMove(Rigidbody2D rigidBody) {
		
		if(serial == null) {
			if(Input.GetKey(keyRight)) {
				movementDirection = 1;
			} else if(Input.GetKey(keyLeft)) {
				movementDirection = -1;
			} else {
				movementDirection = 0;
			}
		} else {
			if((byteRead & (1 << 1)) == 2) {
				 movementDirection = -1;
			} else if((byteRead & (1 << 0)) == 1) {
				 movementDirection = 1;
			} else {
				movementDirection = 0;
			}
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
		if (levelManager.playerOneCharacters.Count > 1 && levelManager.playerTwoCharacters.Count > 1) {
			Vector2 characterPosition = new Vector2 (transform.position.x, transform.position.y);
			GameObject newCharacter = GameObject.Instantiate (otherCharacter, new Vector2 (characterPosition.x, characterPosition.y), Quaternion.identity) as GameObject;
			if (playerNum == 1) {
				levelManager.PlayerOneSwap (newCharacter);
			} else if (playerNum == 2) {
				levelManager.PlayerTwoSwap (newCharacter);
			}

			Destroy (gameObject);
		}
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
		setSerialPort(playerNum);
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
	protected void  setSerialPort(int playerNum) {
		if(playerNum == 1) {
			serial = levelManager.serial1;
		} else if(playerNum == 2) {
			serial = levelManager.serial2;
		}
	}
	protected void flip() {
		facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
	}
	
	protected void OnCollisionEnter2D(Collision2D other) {
		if((other.gameObject.name == "water" || other.gameObject.layer == 4) && this.gameObject.tag != "swimmer") {
			levelManager.RespawnPlayers();
		}
		 
		if(other.gameObject.name == "laserBullet" || other.gameObject.name == "playerHazard" || other.gameObject.tag == "spikes") {
			GetComponent<SpriteRenderer>().enabled = false;
			levelManager.RespawnPlayers();
		}
		
		if(other.gameObject.tag == "Player" || other.gameObject.name == "swimmerCharacter") {
			Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>());
		}
	}

	protected void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "SpawnPoint") {
			SPManager.playersInSpawnPoint++;
			print ("Players in trigger " + SPManager.playersInSpawnPoint);
			if (SPManager.playersInSpawnPoint == 2) {
				SPManager.playersInSpawnPoint = 0;
				levelManager.SetNewSpawnPoint ();
				Destroy (other.gameObject);
			}
		}
		
		if(other.gameObject.tag == "teleporter") {
			levelManager.SetPlayerAtTeleporter(playerNum, true);
		}
	}

	protected void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "SpawnPoint") {
			if (SPManager.playersInSpawnPoint != 0) {
				SPManager.playersInSpawnPoint--;
				print ("Players in trigger " + SPManager.playersInSpawnPoint);
			}
		}
		
		if(other.gameObject.tag == "teleporter") {
			levelManager.SetPlayerAtTeleporter(playerNum, false);
		}
	}
}
