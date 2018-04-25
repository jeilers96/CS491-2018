using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBulletBehavior : MonoBehaviour {
	protected float directionMod;
	
	private Rigidbody2D rigid2D;
	private float initialX;
	private float initialY;

	// Use this for initialization
	void Start () {
		gameObject.name = "laserBullet";
		rigid2D = GetComponent<Rigidbody2D>();
		initialX = rigid2D.position.x;
		initialY = rigid2D.position.y;
	}
	
	void Update () {
		if(Mathf.Abs(rigid2D.position.x - initialX) >= 20.0) {
			Destroy(gameObject);
		}
		
		if(Mathf.Abs(rigid2D.position.y - initialY) >= 20.0) {
			Destroy(gameObject);
		}
	}
	
	protected void OnCollisionEnter2D() {
		Debug.Log("Bullet collision");
		Destroy(gameObject);
	}
	
	protected void OnTriggerEnter2D() {
		Destroy(gameObject);
	}
}
