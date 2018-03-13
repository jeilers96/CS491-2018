using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprinterController : ingameCharacter {
	private Rigidbody2D rigid2D;
	
	const float DEFAULT_SPRINT_MODIFIER = 3.0f;

	// Use this for initialization
	void Start () {
		gameObject.name = "sprinterCharacter";
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		base.FixedUpdate();
		playerMove(rigid2D);
	}
	
	void Update () {
		if(isGrounded && Input.GetKeyDown(keyJump)) {
			playerJump(rigid2D);
		}
		
		if(Input.GetKeyDown(keyAction)) {
			playerAction(rigid2D);
		}
		else if(Input.GetKeyUp(keyAction)) {
			resetPlayerState();
		}
		
		if(Input.GetKeyDown(keySwap)) {
			swapCharacter(gameObject);
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
		moveSpeed *= DEFAULT_SPRINT_MODIFIER;
	}
	
	public override void resetPlayerState() {
		moveSpeed = DEFAULT_MOVE_SPEED;	
	}
}
