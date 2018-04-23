using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetController : MonoBehaviour {
	public doorTrigger[] doorTrig;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "projectile") {
			foreach(doorTrigger trigger in doorTrig) {	
				trigger.Toggle(true);
			}
		}
	}
}
