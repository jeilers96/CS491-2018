using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swimmerController : ingameCharacter {
	private Rigidbody2D rigid2D;

	// Use this for initialization
	void Start () {
		gameObject.name = "swimmerCharacter";
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
			
			//switch character (no hardware)
			if(Input.GetKeyDown(keySwap)) {
				swapCharacter();
			}
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
	}
	
	public override void resetPlayerState() {
	}
}
