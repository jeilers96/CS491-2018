using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : ingameCharacter {
	private Rigidbody2D rigid2D;

	// Use this for initialization
	void Start () {
		gameObject.name = "gravityCharacter";
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		//check if character is grounded
		grounded();
		
		if(serial != null) {
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
		rigid2D.gravityScale *= -1.0f;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1.0f, transform.localScale.z);
	}
	
	public override void resetPlayerState() {
	}
}
