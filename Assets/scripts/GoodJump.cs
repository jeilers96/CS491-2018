using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodJump : MonoBehaviour {

	[Range(1,50)]
	public float jumpVelocity;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	public float maxSpeed = 10f;
	bool facingRight = true;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 300f;

	Rigidbody2D rb;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis ("Horizontal");

		rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

		if(Mathf.Abs(move) > 0 && grounded) {
			GetComponent<Animator> ().SetBool ("walking",true);
			GetComponent<Animator> ().SetBool ("jumping",false);
		} else if(!grounded){
			GetComponent<Animator> ().SetBool ("walking",false);
			GetComponent<Animator> ().SetBool ("jumping",true);
		} else{
			GetComponent<Animator> ().SetBool ("walking",false);
			GetComponent<Animator> ().SetBool ("jumping",false);
		}

		if(move > 0 && !facingRight) {
			flip();
		} else if(move < 0 && facingRight){
			flip ();
		} 
		if (Input.GetButtonDown ("Jump")) {
			rb.velocity = Vector2.up * jumpVelocity * 2;
		}

		if (rb.velocity.y < 0) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

	void flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
