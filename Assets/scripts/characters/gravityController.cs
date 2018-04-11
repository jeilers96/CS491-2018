using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : ingameCharacter {
	private Rigidbody2D rigid2D;
	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		gameObject.name = "gravityCharacter";
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
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
			swapCharacter();
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
		rigid2D.gravityScale *= -1.0f;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1.0f, transform.localScale.z);
	}
	
	public override void resetPlayerState() {
	}
}
