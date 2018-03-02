using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketeerController : ingameCharacter {
	Rigidbody2D rigid2D;
	
	private ingameCharacter player;
	
	const float DEFAULT_SPRINT_MODIFIER = 3.0f;

	// Use this for initialization
	void Start () {
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		base.FixedUpdate();
		playerMove(rigid2D, movementDirection);
	}
	
	void Update () {
		if(isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) {
			playerJump(rigid2D);
		}
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			playerAction(rigid2D);
		}
		else if(Input.GetKeyUp(KeyCode.Space)) {
			resetPlayerState();
		}
		
		if(Input.GetKeyDown(KeyCode.E)) {
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
