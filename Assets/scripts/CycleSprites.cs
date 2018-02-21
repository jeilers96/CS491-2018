using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Cycles between the various sprites for a conveyor in order to make it look like an animation.
/// </summary>
public class CycleSprites : MonoBehaviour {

	public float resetTime = 0.25f;
	public List<Sprite> sprites;

	private int i = 0;
	private float currentTime = 0.0f;
	private SurfaceEffector2D effector;

	void Start(){
		effector = GetComponentInParent<SurfaceEffector2D> ();
	}

	// Update is called once per frame
	void Update(){
		currentTime += Time.deltaTime;
		if (currentTime >= resetTime) {

			if (effector.speed > 0) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [i];
				i++;
				currentTime = 0;
				if (i == sprites.Count) {
					i = 0;
				} 
			} else {
				gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [i];
				i--;
				currentTime = 0;
				if (i < 0) {
					i = sprites.Count - 1;
				}
			}
		} 
	}
}