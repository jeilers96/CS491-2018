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
	Animator animator; 

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis ("Horizontal");

		rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

		if(Mathf.Abs(move) > 0 && grounded) {
			animator.SetBool ("walking",true);
			animator.SetBool ("jumping",false);
		} else if(!grounded){
			animator.SetBool ("walking",false);
			animator.SetBool ("jumping",true);
		} else{
			animator.SetBool ("walking",false);
			animator.SetBool ("jumping",false);
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
