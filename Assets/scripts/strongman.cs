using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strongman : MonoBehaviour {
	
	public bool grabbed;
	RaycastHit2D hit;
	public float distance=2f;
	public Transform holdpoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Space))
		{

			if(!grabbed)
			{
				Physics2D.queriesStartInColliders = false;

				hit = Physics2D.Raycast(transform.position,Vector2.right*transform.localScale.x,distance);

				if(hit.collider != null && hit.collider.tag == "heavy_box")
				{
					grabbed = true;

				}
			}else 
			{
				grabbed=false;

			}


		}

		if (grabbed) {
			hit.collider.gameObject.transform.position = holdpoint.position;
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position,transform.position+Vector3.right*transform.localScale.x*distance);
	}
}
	