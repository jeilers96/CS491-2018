using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ingameCharacter : MonoBehaviour {
	//public float moveSpeed = 3.0f;
	//public float jumpForce = 250.0f;
	public float moveSpeed;
	public float jumpForce;
	public Transform groundCheck;
	public LayerMask groundEntity;
	
	protected float movementDirection;
	protected bool isGrounded = false;
	protected float groundRadius = 0.2f;
	
	const float DEFAULT_MOVE_SPEED = 3.0f;
	const float DEFAULT_JUMP_FORCE = 250.0f;
	
	// Use this for initialization
	protected void Start () {
		moveSpeed = DEFAULT_MOVE_SPEED;
		jumpForce = DEFAULT_JUMP_FORCE;
	}
	
	// Update is called once per frame
	protected void FixedUpdate () {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundEntity);
	}
	
	protected void playerMove(Rigidbody2D rigidBody, float movementDirection) {
		movementDirection = Input.GetAxis("Horizontal");
		rigidBody.velocity = new Vector2(movementDirection * moveSpeed, rigidBody.velocity.y);
	}
	
	protected void playerJump(Rigidbody2D rigidBody) {
		rigidBody.AddForce(new Vector2(0, jumpForce));
	}
	
	public abstract void playerAction(Rigidbody2D rigidBody);
	
	protected void resetPlayerState(Rigidbody2D rigidBody) {
		moveSpeed = DEFAULT_MOVE_SPEED;
		jumpForce = DEFAULT_JUMP_FORCE;
	}
}
