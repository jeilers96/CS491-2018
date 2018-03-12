using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformTrigger : MonoBehaviour {

	public rising_platform platform;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Toggle(bool state) {
		if(state) {
			platform.rise ();
		} else {
			platform.fall ();
		}
	}
}
