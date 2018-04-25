using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkerController : ingameCharacter {
	public GameObject normalCharacter;
	public GameObject shrunkCharacter;
	public bool isShrunk;
	
	private Rigidbody2D rigid2D;
	const float DEFAULT_SHRINK_OFFSET = 0.64f;

	// Use this for initialization
	void Start () {
		gameObject.name = "shrinkerCharacter";
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		//check if character is grounded
		grounded();
		
		if(levelManager.serial != null) {
			//get input from hardware 
			getBytesFromInput();
			//get input and move player accordingly 
			playerMove(rigid2D);
			// jump and action (hardware)
			if((byteRead & (1 << 2)) == 4 && isGrounded) {
				 playerJump(rigid2D);
				 byteRead = byteRead & ~(1 << 2);
			}
			
			if((byteRead & (1 << 4)) == 16) {
				 playerAction(rigid2D);
				 byteRead = byteRead & ~(1 << 4);
			}else {
				resetPlayerState();
			}
			//switch character (hardware)
			if((byteRead & (1 << 5)) == 32) {
				 swapCharacter();
				 byteRead = byteRead & ~(1 << 5);
			}
			
		} else {
			
			playerMove(rigid2D);
			//jump and action (no hardware)
			if(isGrounded && Input.GetKeyDown(keyJump)) {
				playerJump(rigid2D);
			}
			
			if(Input.GetKeyDown(keyAction)) {
				playerAction(rigid2D);
			}
			else if(Input.GetKeyUp(keyAction)) {
				resetPlayerState();
			}
			
			//switch character (no hardware)
			if(Input.GetKeyDown(keySwap)) {
				swapCharacter();
			}
		}
		
		
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
		Vector2 characterPosition = new Vector2(transform.position.x, transform.position.y);
		if(!isShrunk) {
			GameObject.Instantiate(shrunkCharacter, new Vector2(characterPosition.x, characterPosition.y - DEFAULT_SHRINK_OFFSET), Quaternion.identity);
		}
		else {
			GameObject.Instantiate(normalCharacter, new Vector2(characterPosition.x, characterPosition.y + DEFAULT_SHRINK_OFFSET), Quaternion.identity);
		}

		Destroy(gameObject);
	}
	
	public override void resetPlayerState() {
	}
}
