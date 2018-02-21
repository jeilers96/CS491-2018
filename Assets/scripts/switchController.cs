using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchController : MonoBehaviour {
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay2D() {
		
		anim.SetBool("down",true);
	}
	
	void OnTriggerExit2D() {
		
		anim.SetBool("down",false);
		
	}
}
