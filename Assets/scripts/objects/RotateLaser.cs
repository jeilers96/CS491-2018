using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour {
	public GameObject laserToRotate;
	public float rotateAmount = 0;
	public bool stayRotated = true;

	private bool rotated = false;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D() {
		if (laserToRotate == null) {
			print ("laserToRototate needs to be assigned:");
		} else {
			if (!rotated) {
				laserToRotate.transform.Rotate (0, 0, rotateAmount);
				rotated = true;
				turretBehavior tb = laserToRotate.gameObject.GetComponent<turretBehavior> ();
				if (tb != null) {
					tb.ShootBullet ();
					tb.enabled = false;
				}
			}
		}

		anim.SetBool("down",true);
	}

	void OnTriggerExit2D() {
		if (laserToRotate == null) {
			print ("laserToRototate needs to be assigned:");
		} else {
			if (!stayRotated) {
				laserToRotate.transform.Rotate (0, 0, -rotateAmount);
			}
		}

		anim.SetBool("down",false);
	}
}
