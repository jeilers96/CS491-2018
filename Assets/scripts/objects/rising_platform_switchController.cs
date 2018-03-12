using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rising_platform_switchController : MonoBehaviour {
	Animator anim;
	public platformTrigger[] platformTrig;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay2D() {
		
		anim.SetBool("down",true);
		foreach(platformTrigger trigger in platformTrig) {
			
			trigger.Toggle(true);
		}
	}
	
	void OnTriggerExit2D() {
		
		anim.SetBool("down",false);
		foreach(platformTrigger trigger in platformTrig) {
			
			trigger.Toggle(false);
		}
		
	}
}
