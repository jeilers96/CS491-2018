using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour {
	public Transform laserToRotate;
	public float rotateAmount = 0;
	public bool stayRotated = true;

	private bool rotated = false;

	void OnTriggerEnter2D() {
		if (laserToRotate != null && !rotated) {
			laserToRotate.Rotate (0, 0, rotateAmount);
			rotated = true;
			turretBehavior tb = laserToRotate.gameObject.GetComponent<turretBehavior> ();
			if (tb != null) {
				tb.ShootBullet ();
				tb.enabled = false;
			}
		} else {
			print ("no transform assigned on button: " + gameObject.name);
		}
	}

	void OnTriggerExit2D() {
		if (laserToRotate != null && !stayRotated) {
			laserToRotate.Rotate (0, 0, -rotateAmount);
		} else {
			print ("no transform assigned on button: " + gameObject.name);
		}
	}
}
