using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBehavior : MonoBehaviour {
	const float DEFAULT_SHOT_TIME = 1.5f;
	
	public float TimeBetweenShots = DEFAULT_SHOT_TIME;
	public float Offset = 0;
	public float bulletSpeed = 9f;
	public GameObject laserBullet;

	private float currentTime = 0;
	private bool shotOnce = false;
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if (!shotOnce && currentTime >= Offset) {
			ShootBullet (true);
		}

		if (currentTime >= TimeBetweenShots) {
			ShootBullet ();
		}
	}

	public void ShootBullet(bool setShotOnce = false){
		GameObject bullet = Instantiate (laserBullet, transform.position, Quaternion.identity);
		bullet.transform.rotation = transform.rotation;
		bullet.transform.Rotate (0, 0, 90);
		bullet.GetComponent<Rigidbody2D> ().velocity = -transform.up * bulletSpeed;

		if (setShotOnce) {
			shotOnce = true;
		}

		currentTime = 0;
	}
}
