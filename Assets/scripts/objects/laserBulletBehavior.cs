using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBulletBehavior : MonoBehaviour {

	private Rigidbody2D rigid2D;
	private float initialX;

	// Use this for initialization
	void Start () {
		gameObject.name = "laserBullet";
		rigid2D = GetComponent<Rigidbody2D>();
		initialX = rigid2D.position.x;
		rigid2D.velocity = new Vector2(-10,0);
	}
	
	void Update () {
		if(Mathf.Abs(rigid2D.position.x - initialX) >= 20.0) {
			Destroy(gameObject);
		}
	}
	
}
