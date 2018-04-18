using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBehavior : MonoBehaviour {
	const int DEFAULT_CYCLE_TIME = 120;
	const int DEFAULT_INITIAL_SHOT_CLOCK = 0;
	
	public int cycleTime = DEFAULT_CYCLE_TIME;
	public int initialShotClock = DEFAULT_INITIAL_SHOT_CLOCK;
	public string direction = "left";		// Possible options include : up, down, left, right
	public bool orientation = true;				// vertical = true, horizontal = false
	public GameObject laserBullet;
	
	private float shotDirection;
	private int shotClock = 0;
	private float diagonalDirection;
	
	// Use this for initialization
	void Start () {
		shotClock = initialShotClock;
	}
	
	// Update is called once per frame
	void Update () {
		if(shotClock == cycleTime) {
			GameObject bullet = Instantiate(laserBullet, transform.position, Quaternion.identity);
			
			if(direction.Equals("up") || direction.Equals("down")){
				bullet.transform.Rotate(0, 0, 90);
			}
			
			if (direction.Equals ("up") || direction.Equals ("right")) {
				shotDirection = 1.0f;
			} else if (direction.Equals ("down") || direction.Equals ("left")) {
				shotDirection = -1.0f;
			} else if (direction.Equals ("downleft")) {
				bullet.transform.Rotate (0, 0, 45);
			} else {
				Destroy (gameObject);
				Debug.Log ("Invalid direction for turret provided.  Please check spelling of given direction.");
			}
			
			//bullet.GetComponent<laserBulletBehavior>().setVelocity(shotDirection, orientation);
			
			setVelocity(bullet, shotDirection, orientation);
			
			shotClock = 0;
		}
		else {
			shotClock++;
		}
	}
	
	private void setVelocity(GameObject newBullet, float directionMod, bool orientation) {
		if(orientation) {
			newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * directionMod, 0);
		}
		else {
			newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10 * directionMod);
		}
	}
}
