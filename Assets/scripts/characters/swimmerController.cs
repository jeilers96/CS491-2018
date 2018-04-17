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
	
	// Update is called once per frame
	void FixedUpdate () {
		base.FixedUpdate();
		playerMove(rigid2D);
	}
	
	void Update () {
		//check if character is grounded
		grounded();
		//get input from hardware 
		getBytesFromInput();
		//get input and move player accordingly 
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
		
		// jump and action (hardware)
		if((byteRead & (1 << 2)) == 4 && isGrounded) {
			 playerJump(rigid2D);
			 byteRead = byteRead & ~(1 << 2);
		}
		
		if((byteRead & (1 << 4)) == 16) {
			 playerAction(rigid2D);
		}else if((byteRead & (1 << 4)) == 0) {
			resetPlayerState();
		}
		
		//switch character (no hardware)
		if(Input.GetKeyDown(keySwap)) {
			swapCharacter();
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
	}
	
	public override void resetPlayerState() {
	}
}
