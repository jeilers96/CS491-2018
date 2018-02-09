using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_test_controller : MonoBehaviour {
	
	public float maxSpeed = 10f;
	bool facingRight = true;
	bool grounded = false;
	public Transform groundCheck;
	Rigidbody2D rigid2D;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 300f;
	
	// Use this for initialization
	void Start () {
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis ("Horizontal");
		
		rigid2D.velocity = new Vector2(move * maxSpeed, rigid2D.velocity.y);
		
		if(move > 0 && !facingRight) {
			flip();
		} else if(move < 0 && facingRight){
			flip ();
		}
	}
	
	void Update(){
		if(grounded && Input.GetKeyDown(KeyCode.UpArrow)) {
			rigid2D.AddForce(new Vector2(0, jumpForce));
		}
	}
	
	void flip() {
		
		facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
	}
}
