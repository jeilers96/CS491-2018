using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformTrigger : MonoBehaviour {

	public rising_platform platform;

	public void Toggle(bool state) {
		if(state) {
			platform.rise ();
		} else{
			platform.fall ();
		}
	}
}
