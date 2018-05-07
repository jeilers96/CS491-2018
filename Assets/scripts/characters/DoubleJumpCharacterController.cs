using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCharacterController : ingameCharacter {
	public float jumpDelay = 0.1f;

	private Rigidbody2D rigid2D;
	private bool hasSecondJump;
	private float jumpStartTime;

	// Use this for initialization
	void Start () {
		gameObject.name = "DoubleJumpCharacter";
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
		hasSecondJump = false;
	}

	void Update () {
		grounded();
		//get input and move player accordingly 
		if(serial != null) {
			//get input from hardware 
			getBytesFromInput();
			//get input and move player accordingly 
			playerMove(rigid2D);
			// jump and action (hardware)
			if((byteRead & (1 << 2)) == 4 && isGrounded) {
				isGrounded = false;
				playerJump (rigid2D);
				jumpStartTime = Time.time + jumpDelay;
				hasSecondJump = true;
				byteRead = byteRead & ~(1 << 2);
			} else if (hasSecondJump && !isGrounded && (byteRead & (1 << 2)) == 4 && (Time.time > jumpStartTime)) {
				print("second jump");
				playerJump (rigid2D);
				hasSecondJump = false;
				byteRead = byteRead & ~(1 << 2);
			}
			
			//switch character (hardware)
			if((byteRead & (1 << 5)) == 32) {
				 swapCharacter();
				 byteRead = byteRead & ~(1 << 5);
			}
		} else {
			playerMove(rigid2D);
			if(isGrounded && Input.GetKeyDown(keyJump)) {
				isGrounded = false;
				playerJump (rigid2D);
				jumpStartTime = Time.time + jumpDelay;
				hasSecondJump = true;
			} else if (hasSecondJump && !isGrounded && Input.GetKeyDown(keyJump) && (Time.time > jumpStartTime)) {
				playerJump (rigid2D);
				hasSecondJump = false;
			}
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

