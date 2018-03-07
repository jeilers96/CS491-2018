using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTrigger : MonoBehaviour {

	public door door;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Toggle(bool state) {
		if(state) {
			door.DoorOpens ();
		} else {
			door.DoorCloses ();
		}
	}
	void onTriggerEnter2D(Collider2D other) {
		if(other.tag=="player") {
			door.DoorOpens ();
		}
	}
	
	void onTriggerExit2D(Collider2D other) {
		if(other.tag=="player") {
			door.DoorCloses ();
		}
	}
}
