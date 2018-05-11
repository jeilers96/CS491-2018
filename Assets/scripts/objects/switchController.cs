using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchController : MonoBehaviour {
	Animator anim;
	public doorTrigger[] doorTrig;
	private bool isPressed = false;
	private string lastColliderName = null;
	private string colliderEnterName = null;
	private string colliderExitName = null;

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		colliderEnterName = col.gameObject.name;

		if(!isPressed){
			lastColliderName = colliderEnterName;
			if (!anim.GetBool ("down")) {
				foreach (doorTrigger trigger in doorTrig) {

					trigger.Toggle (true);
				}

				isPressed = !isPressed;
			}
				
			anim.SetBool("down",true);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		colliderExitName = col.gameObject.name;

		if (lastColliderName == colliderExitName && isPressed) {
			isPressed = !isPressed;
			if (anim.GetBool ("down")) {
				foreach (doorTrigger trigger in doorTrig) {

					trigger.Toggle (false);
				}
			}

			anim.SetBool("down",false);
		}
	}
}
