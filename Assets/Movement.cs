using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float speed;
	public Vector3 jump;
	public float jumpForce = 3.0f;
	public bool canDoubleJump;

	private Rigidbody2D rb;
	private BoxCollider2D collider;
	private float distToGround;
	private bool isGrounded;
	private Vector3 moveDirection;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D> ();
		jump = new Vector3(0.0f, 2.0f, 0.0f);
		canDoubleJump = true;
		isGrounded = true;
	}
		
	//make sure u replace "floor" with your gameobject name.on which player is standing
	void OnCollisionEnter2D(){
		print ("entering");
		isGrounded = true;
	}

	//consider when character is jumping .. it will exit collision.
	void OnCollisionExit2D(){
		print ("exiting");
		isGrounded = false;
	}

	void Update ()
	{
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed; 
		transform.Translate (moveDirection);

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
			if (isGrounded) {
				rb.velocity = new Vector2(rb.velocity.x, 0);
				rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
				canDoubleJump = true;
				isGrounded = false;
			} else {
				if (canDoubleJump) {
					canDoubleJump = false;
					rb.velocity = new Vector2(rb.velocity.x, 0);
					rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
				}
			}
		}
	}
}
