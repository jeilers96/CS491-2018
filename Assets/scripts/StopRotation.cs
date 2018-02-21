using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//prevents the objects this is attached to from rotating
public class StopRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody2D rb2D = GetComponent<Rigidbody2D> ();
		rb2D.freezeRotation = true;
	}
}
