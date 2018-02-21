using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates scaffold according to how many pieces and in what direction, can be managed by the scaffold editor script.
/// </summary>
public class CreateTerrain : MonoBehaviour {

	public int numberOfPieces = 2;
	private int direction = 0;
	public int Direction{
		set{ direction = value; }
	}
	public GameObject ObjectToCreate;
	private GameObject[] allObjects;

	/// <summary>
	/// Builds the scaffold using the number of pieces, the given scaffolding, and the direction.
	/// </summary>
	public void BuildTerrain(){
		allObjects = new GameObject[numberOfPieces];
		RectTransform scaffoldRect = ObjectToCreate.GetComponent<RectTransform> ();
		float height = scaffoldRect.rect.height;
		float width = scaffoldRect.rect.width;
		Vector3 scale = scaffoldRect.transform.localScale;
		// for every piece of scaffold, instantiate and position it line according to direction
		for (int i = 0; i < numberOfPieces; i++) {
			GameObject newObject = Instantiate (ObjectToCreate);
			// set parent and position to center of parent
			newObject.transform.SetParent (transform);
			newObject.transform.position = transform.position;

			allObjects [i] = newObject;

			//modify its position according to the direction
			newObject.transform.position = CalculateNewPosition (newObject.transform.position, width, height, scale, i);
		}
	}

	/// <summary>
	/// Calculates the new position for each piece of scaffold.
	/// </summary>
	/// <returns>The new position.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	/// <param name="scale">Scale.</param>
	/// <param name="index">Index.</param>
	private Vector3 CalculateNewPosition(Vector3 pos, float width, float height, Vector3 scale, int index){
		Vector3 newPosition = pos;
		switch (direction) {
		//vertical
		case 0:
			newPosition.y -= height * scale.y * index;
			break;
			//horizontal
		case 1:
			newPosition.x += width * scale.x * index;
			break;
			//diagonal right
		case 2:
			newPosition.y += height * scale.y * index;
			newPosition.x += width * scale.x * index;
			break;
			//diagonal left
		case 3:
			newPosition.y += height * scale.y * index;
			newPosition.x -= width * scale.x * index;
			break;
		default:
			print ("Error, unknown direction");
			break;
		}
		return newPosition;
	}

	/// <summary>
	/// Removes the built scaffold to reset it
	/// </summary>
	public void DeleteTerrain(){
		foreach(Transform child in transform){
			DestroyImmediate (child.gameObject);
		}
	}
}
