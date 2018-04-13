using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchController : MonoBehaviour {
	Animator anim;
	public doorTrigger[] doorTrig;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	void OnTriggerEnter2D() {
		if (!anim.GetBool ("down")) {
			foreach (doorTrigger trigger in doorTrig) {

				trigger.Toggle (true);
			}
		}

		anim.SetBool("down",true);
	}
	
	void OnTriggerExit2D() {
		if (anim.GetBool ("down")) {
			foreach (doorTrigger trigger in doorTrig) {

				trigger.Toggle (false);
			}
		}

		anim.SetBool("down",false);

	}
}
