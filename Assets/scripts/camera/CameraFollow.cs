using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float smoothSpeed = 0.125f;
	public float maxCameraSize = 10f;
	public float minCameraSize = 7.5f;
	public Vector3 offset = new Vector3(0.0f, 2.5f, -10.0f);

	private LevelManager levelManager;

	private Vector3 playerOneDesiredPosition;
	private Vector3 playerTwoDesiredPosition;
	private Vector3 playerRequiredPosition;

	private Camera mainCamera;
	private Vector3 averagePlayerPosition;

	void Start(){
		levelManager = LevelManager.instance;
		mainCamera = Camera.main;
		transform.position = levelManager.playerOne.position;
	}

	void FixedUpdate(){
		print ("Running here");
		levelManager.playerOne.position = KeepPlayerInCameraBounds (levelManager.playerOne);
		levelManager.playerTwo.position = KeepPlayerInCameraBounds (levelManager.playerTwo);

		playerOneDesiredPosition = levelManager.playerOne.position + offset;
		playerTwoDesiredPosition = levelManager.playerTwo.position + offset;
		averagePlayerPosition = (playerOneDesiredPosition + playerTwoDesiredPosition) / 2;
		print ("Average pos: " + averagePlayerPosition);
		transform.position = Vector3.Lerp (transform.position, averagePlayerPosition, smoothSpeed);
	}

	/// <summary>
	/// Keeps the given player within camera bounds.
	/// </summary>
	/// <returns>Vector3 between 0 and 1, 0 being the leftmost point of the camera.</returns>
	/// <param name="player">Player.</param>
	Vector3 KeepPlayerInCameraBounds(Transform player){
		playerRequiredPosition = mainCamera.WorldToViewportPoint (player.position);

		if ((playerRequiredPosition.x >= .9 || playerRequiredPosition.x <= .1 || playerRequiredPosition.y >= .9 || playerRequiredPosition.y <= .1) && mainCamera.orthographicSize < maxCameraSize) {
			ChangeCameraSize (0.20f);
		} else if ((playerRequiredPosition.x >= .35 || playerRequiredPosition.x <= .65 || playerRequiredPosition.y >= .35 || playerRequiredPosition.y <= .65) && mainCamera.orthographicSize > minCameraSize) {
			ChangeCameraSize (-0.20f);
		}

		playerRequiredPosition = mainCamera.WorldToViewportPoint (player.position);

		//clamp between .05 and .95 instead of 0 and 1
		playerRequiredPosition.x = Mathf.Clamp01(playerRequiredPosition.x);
		playerRequiredPosition.y = Mathf.Clamp01(playerRequiredPosition.y);
		return mainCamera.ViewportToWorldPoint(playerRequiredPosition);
	}

	private void ChangeCameraSize(float amount){
		float size = mainCamera.orthographicSize;
		Vector2 previousSize = new Vector2 (size, 0.0f);
		Vector2 newSize = new Vector2 (size + amount, 0.0f);
		mainCamera.orthographicSize = Vector2.Lerp (previousSize, newSize, smoothSpeed).x;
	}
}
