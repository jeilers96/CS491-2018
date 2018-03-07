using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour {

	public Transform target;
	public float smoothSpeed = 0.01f;
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 desiredPosition = target.position;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}
}
