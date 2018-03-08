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
		
		if(isGrabbing) {
			raycastHit.collider.gameObject.transform.position = holdPoint.position;
		}
		else
		{
			raycastHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, GRAB_DISTANCE);
		}
	}
	
	public override void playerAction(Rigidbody2D rigidBody) {
		if(!isGrabbing)
		{
			Physics2D.queriesStartInColliders = false;

			if(raycastHit.collider != null && raycastHit.collider.tag == "heavy_box")
			{
				isGrabbing = true;
			}
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
