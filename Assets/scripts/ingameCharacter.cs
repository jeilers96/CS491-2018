using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class ingameCharacter : MonoBehaviour {
	public float moveSpeed;
	public float jumpForce;
	public Transform groundCheck;
	public LayerMask groundEntity;
	public Object otherCharacter;
	
	protected float movementDirection;
	protected bool facingRight = true;
	protected bool isGrounded = false;
	protected float groundRadius = 0.2f;
	
	protected const float DEFAULT_MOVE_SPEED = 3.0f;
	protected const float DEFAULT_JUMP_FORCE = 250.0f;
	
	// Use this for initialization
	protected void Start () {
		Rigidbody2D rigid2D = GetComponent<Rigidbody2D> ();
		rigid2D.freezeRotation = true;
		
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
		
		if(Mathf.Abs(movementDirection) > 0 && isGrounded) {
			GetComponent<Animator> ().SetBool ("walking",true);
			GetComponent<Animator> ().SetBool ("jumping",false);
		} else if(!isGrounded){
			GetComponent<Animator> ().SetBool ("walking",false);
			GetComponent<Animator> ().SetBool ("jumping",true);
		} else{
			GetComponent<Animator> ().SetBool ("walking",false);
			GetComponent<Animator> ().SetBool ("jumping",false);
		}
		
		if(movementDirection > 0 && !facingRight) {
			flip();
		} else if(movementDirection < 0 && facingRight){
			flip ();
		}
	}
	
	protected void playerJump(Rigidbody2D rigidBody) {
		rigidBody.AddForce(new Vector2(0, jumpForce));
	}
	
	public abstract void playerAction(Rigidbody2D rigidBody);
	
	public abstract void resetPlayerState();
	
	protected void swapCharacter(GameObject character) {
		Vector2 characterPosition = new Vector2(transform.position.x, transform.position.y);
		GameObject.Instantiate(otherCharacter, new Vector2(characterPosition.x, characterPosition.y), Quaternion.identity);
		Destroy(gameObject);
	}
	
	protected void flip() {
		facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
	}
	
	protected void OnCollisionEnter2D(Collision2D other) {
		 if(other.gameObject.name == "water" && this.gameObject.tag != "swimmer") {
			GetComponent<SpriteRenderer>().enabled = false;
			Application.LoadLevel ("swimmer_test_scene");
			
		 }
	}
}
