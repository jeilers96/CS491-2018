using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rising_platform : MonoBehaviour {
	
	public Vector3 posA;
	public Vector3 posB;
	public float speedUp = 0.05f;
	public float speedDown = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void rise() {
		transform.position = Vector3.MoveTowards(transform.position, posB, speedUp);
	}
	
	public void fall() {
		transform.position = Vector3.MoveTowards(transform.position, posA, speedDown);
	}
}
