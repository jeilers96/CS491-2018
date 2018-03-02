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
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay2D() {
		
		anim.SetBool("down",true);
		foreach(doorTrigger trigger in doorTrig) {
			
			trigger.Toggle(true);
		}
	}
	
	void OnTriggerExit2D() {
		
		anim.SetBool("down",false);
		foreach(doorTrigger trigger in doorTrig) {
			
			trigger.Toggle(false);
		}
		
	}
}
