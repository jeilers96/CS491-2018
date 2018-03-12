using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	private LevelManager levelManager;

	void Start(){
		levelManager = LevelManager.instance;
	}

	void FixedUpdate(){
		if (levelManager.playerOne != null) {
			Vector3 desiredPosition = levelManager.playerOne.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
			transform.position = smoothedPosition;
		}

		if (levelManager.playerTwo != null) {
			Vector3 player2RequiredPos = Camera.main.WorldToViewportPoint (levelManager.playerTwo.position);
			player2RequiredPos.x = Mathf.Clamp01(player2RequiredPos.x);
			player2RequiredPos.y = Mathf.Clamp01(player2RequiredPos.y);
			levelManager.playerTwo.position = Camera.main.ViewportToWorldPoint(player2RequiredPos);
		}
	}
}
