using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketeerController : ingameCharacter {
	Rigidbody2D rigid2D;
	public float sprintModifier = 3.0f;
	private ingameCharacter player;

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
			defaultPlayerState(rigid2D);
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
		rigidBody.velocity = new Vector2(movementDirection * moveSpeed * sprintModifier, rigidBody.velocity.y);
		
	}
}
