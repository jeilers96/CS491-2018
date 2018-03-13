using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float smoothSpeed = 0.125f;
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
		levelManager.playerOne.position = KeepPlayerInCameraBounds (levelManager.playerOne);
		levelManager.playerTwo.position = KeepPlayerInCameraBounds (levelManager.playerTwo);

		playerOneDesiredPosition = levelManager.playerOne.position + offset;
		playerTwoDesiredPosition = levelManager.playerTwo.position + offset;
		averagePlayerPosition = (playerOneDesiredPosition + playerTwoDesiredPosition) / 2;
		transform.position = Vector3.Lerp (transform.position, averagePlayerPosition, smoothSpeed);;
	}

	/// <summary>
	/// Keeps the given player within camera bounds.
	/// </summary>
	/// <returns>Vector3 between 0 and 1, 0 being the leftmost point of the camera.</returns>
	/// <param name="player">Player.</param>
	Vector3 KeepPlayerInCameraBounds(Transform player){
		playerRequiredPosition = mainCamera.WorldToViewportPoint (player.position);
		playerRequiredPosition.x = Mathf.Clamp01(playerRequiredPosition.x);
		playerRequiredPosition.y = Mathf.Clamp01(playerRequiredPosition.y);
		return Camera.main.ViewportToWorldPoint(playerRequiredPosition);
	}
}
