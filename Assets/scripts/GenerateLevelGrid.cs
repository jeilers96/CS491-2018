using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a grid out of gameobjects for each position on the map
/// </summary>
public class GenerateLevelGrid : MonoBehaviour {

	public int xAxisSize = 5;
	public int yAxisSize = 5;
	public GameObject generalObject;
	public List<GameObjectArray> xAxis;

	//access elements of 2d jagged array like this
	//GameObject go = xAxis[x].yAxis[y];

	/// <summary>
	/// The start position of where the grid will be built. This position will be the top left spot (0,0) on the grid.
	/// </summary>
	private Vector3 startPosition;

	public void Start(){
		startPosition = transform.position;
	}

	/// <summary>
	/// Builds the level grid by instantiating gameobjects along a jagged array
	/// </summary>
	public void BuildGrid(){
		for (int x = 0; x < xAxis.Count; x++) {
			for (int y = 0; y < xAxis[x].yAxis.Count; y++) {
				GameObject gameObject = Instantiate (xAxis [x].yAxis [y]);

				RectTransform gameObjectRect = gameObject.GetComponent<RectTransform> ();
				float height = gameObjectRect.rect.height;
				float width = gameObjectRect.rect.width;

				Vector3 scale = gameObjectRect.transform.localScale;

				Vector3 newPosition = startPosition;
				newPosition.y += height * scale.y * y;
				newPosition.x += width * scale.y * x;

				gameObject.transform.SetParent (transform);
				gameObject.transform.position = newPosition;
			}
		}
	}

	/// <summary>
	/// Fills the grid with all of the same specified game objects as a 2D array
	/// </summary>
	public void FillGenericGrid(){
		xAxis = new List<GameObjectArray> ();

		for (int x = 0; x < xAxisSize; x++) {
			GameObjectArray gameObjectArray = new GameObjectArray();
			gameObjectArray.yAxis = new List<GameObject> ();

			for (int y = 0; y < yAxisSize; y++) {
				generalObject = new GameObject ("(" + x + "," + y + ")");
				generalObject.transform.SetParent (transform);
				gameObjectArray.yAxis.Add (generalObject);
			}
				
			xAxis.Add (gameObjectArray);
		}
	}

	/// <summary>
	/// Removes the built level grid
	/// </summary>
	public void DeleteGrid(){
		foreach(Transform child in transform){
			DestroyImmediate (child.gameObject);
		}
	}

	/// <summary>
	/// Resets the grid array to be new array. Resetting everything
	/// </summary>
	public void ResetGridArray(){
		xAxis = new List<GameObjectArray> ();
	}
}

[System.Serializable]
public class GameObjectArray
{
	public List<GameObject> yAxis;
}
