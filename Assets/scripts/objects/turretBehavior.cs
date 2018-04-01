using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBehavior : MonoBehaviour {
	public int cycleTime = 120;
	public GameObject laserBullet;
	
	private int shotClock = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(shotClock == cycleTime) {
			GameObject bullet = Instantiate(laserBullet, transform.position, Quaternion.identity);
			shotClock = 0;
		}
		else {
			shotClock++;
		}
	}
}
