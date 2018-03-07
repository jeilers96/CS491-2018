using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCharacterController : ingameCharacter {
	private Rigidbody2D rigid2D;
	private ingameCharacter player;
	private bool hasSecondJump;
	private Animator animator;
	private bool facingRight = true;

	// Use this for initialization
	void Start () {
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
		hasSecondJump = true;
		moveSpeed *= 2.0f;
		jumpForce *= 1.3f;
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		base.FixedUpdate();
		playerMove(rigid2D, movementDirection);
	}

	void Update () {
		if(isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) {
			playerJump (rigid2D);
			hasSecondJump = true;
			print ("first jump");
		}
		else if (hasSecondJump && !isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) {
			playerJump (rigid2D);
			hasSecondJump = false;
			print ("second jump");
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			swapCharacter(gameObject);
		}
	}

	public override void playerAction(Rigidbody2D rigidBody) {

	}

	public override void resetPlayerState() {

	}
}

