using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	//public Vector3 endingPos;
	public Vector3 velocity;
	public float loopTime = 2.0f;

	//private Vector3 startingPos;
	//private Vector3 difference;
	private float currentTime = 0f;
	private bool flip = false;

	private void Start(){
		//startingPos = transform.position;
		//difference = startingPos - endingPos;
	}

	private void OnCollisionEnter2D(Collision2D collision){
		collision.collider.transform.SetParent (transform);
	}

	private void OnCollisionExit2D(Collision2D collision){
		collision.collider.transform.SetParent (null);
	}

	private void FixedUpdate(){
		currentTime += Time.deltaTime;
		if (currentTime >= loopTime) {
			currentTime = 0;
			flip = !flip;
		} else if (!flip) {
			transform.position += (velocity * Time.deltaTime);
		} else {
			transform.position -= (velocity * Time.deltaTime);
		}
	}
}
