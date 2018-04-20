using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strongmanController : ingameCharacter {
	public Transform holdPoint;
	
	private Rigidbody2D rigid2D;
	private RaycastHit2D raycastHit;
	
	private const float GRAB_DISTANCE = 2f;
	private bool isGrabbing;

	// Use this for initialization
	void Start () {
		gameObject.name = "strongmanCharacter";
		base.Start();
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		//check if character is grounded
		grounded();
		
		if(levelManager.serial != null) {
			//get input from hardware 
			getBytesFromInput();
			//get input and move player accordingly 
			playerMove(rigid2D);
			// jump and action (hardware)
			if((byteRead & (1 << 2)) == 4 && isGrounded) {
				 playerJump(rigid2D);
				 byteRead = byteRead & ~(1 << 2);
			}
			
			if((byteRead & (1 << 4)) == 16) {
				 playerAction(rigid2D);
				 byteRead = byteRead & ~(1 << 4);
			}
		} else {
			
			playerMove(rigid2D);
			//jump and action (no hardware)
			if(isGrounded && Input.GetKeyDown(keyJump)) {
				playerJump(rigid2D);
			}
			
			if(Input.GetKeyDown(keyAction)) {
				playerAction(rigid2D);
			}
		}
		//switch character (no hardware)
		if(Input.GetKeyDown(keySwap)) {
			swapCharacter();
		}
		
		if(isGrabbing) {
			raycastHit.collider.gameObject.transform.position = holdPoint.position;
		}
		else
		{
			raycastHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, GRAB_DISTANCE);
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
		if (!isGrabbing) {
			Physics2D.queriesStartInColliders = false;

			if (raycastHit.collider != null && raycastHit.collider.tag == "heavy_box") {
				isGrabbing = true;
			}
		} else {
			isGrabbing = false;
		}
	}
	
	public override void resetPlayerState() {
		if(isGrabbing) {
			isGrabbing = false;
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * GRAB_DISTANCE);
	}
}
