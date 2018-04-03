using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkerController : ingameCharacter {
	public GameObject normalCharacter;
	public GameObject shrunkCharacter;
	public bool isShrunk;
	
	private Rigidbody2D rigid2D;
	
	const float DEFAULT_SHRINK_OFFSET = 0.64f;

	// Use this for initialization
	void Start () {
		gameObject.name = "shrinkerCharacter";
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
			swapCharacter();
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
		Vector2 characterPosition = new Vector2(transform.position.x, transform.position.y);
		if(!isShrunk) {
			GameObject newCharacter = GameObject.Instantiate(shrunkCharacter, new Vector2(characterPosition.x, characterPosition.y - DEFAULT_SHRINK_OFFSET), Quaternion.identity) as GameObject;
		}
		else {
			GameObject newCharacter = GameObject.Instantiate(normalCharacter, new Vector2(characterPosition.x, characterPosition.y + DEFAULT_SHRINK_OFFSET), Quaternion.identity) as GameObject;
		}

		Destroy(gameObject);
	}
	
	public override void resetPlayerState() {
	}
}
