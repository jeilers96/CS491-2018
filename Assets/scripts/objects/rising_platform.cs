using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rising_platform : MonoBehaviour {
	
	public Vector3 posA;
	public Vector3 posB;
	public float speedUp = 0.05f;
	public float speedDown = 2f;

	private Animation anim;

	void Start(){
		anim = GetComponent<Animation> ();
	}
	
	public void rise() {
		if (anim == null) {
			transform.position = Vector3.MoveTowards (transform.position, posB, speedUp);
		} else if (anim != null && !anim.isPlaying) {
			anim.Play ();
		}
	}
	
	public void fall() {
		transform.position = Vector3.MoveTowards(transform.position, posA, speedDown);
	}
}
