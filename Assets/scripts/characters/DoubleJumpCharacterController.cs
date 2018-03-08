﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpCharacterController : ingameCharacter {
	private Rigidbody2D rigid2D;
	private bool hasSecondJump;

	// Use this for initialization
	void Start () {
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
		hasSecondJump = true;
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
		}
		else if (hasSecondJump && !isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) {
			playerJump (rigid2D);
			hasSecondJump = false;
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
