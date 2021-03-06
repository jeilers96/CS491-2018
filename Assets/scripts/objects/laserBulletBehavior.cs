﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBulletBehavior : MonoBehaviour {
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
	
	protected void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name != "laserBullet") {
			Destroy(gameObject);
		}
	}
	
	protected void OnTriggerEnter2D() {
		Destroy(gameObject);
	}
}
