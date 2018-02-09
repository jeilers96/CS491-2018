using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour {
	public float maxSpeed = 3f;
	Rigidbody2D rigid2D;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 250f;
	
	// Use this for initialization
	void Start () {
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		
		float move = Input.GetAxis("Horizontal");
		rigid2D.velocity = new Vector2(move * maxSpeed, rigid2D.velocity.y);
	}
	
	void Update() {
		if(grounded && Input.GetKeyDown(KeyCode.UpArrow)) {
			rigid2D.AddForce(new Vector2(0, jumpForce));
		}
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			maxSpeed *= 3f;
		} else if (Input.GetKeyUp(KeyCode.Space)){
			maxSpeed = 3f;
		}
	}
}
