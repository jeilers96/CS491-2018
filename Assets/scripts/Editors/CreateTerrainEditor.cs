#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Create scaffold editor. Used to easily build scaffold using the unity inspector.
/// </summary>
[CustomEditor(typeof(CreateTerrain))]
public class CreateTerrainEditor : Editor{

	private string[] options = new [] {"Vertical", "Horizontal", "Diag Right", "Diag Left"};
	int choiceIndex = 0;

	/// <summary>
	/// Raises the inspector GU event. Updates the unity inspector to have an options list for direction and a button for creating and deleting scaffold.
	/// </summary>
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		CreateTerrain createTerrain = (CreateTerrain)target;

		choiceIndex = EditorGUILayout.Popup ("Direction", choiceIndex, options);

		createTerrain.Direction = choiceIndex;

		if (GUILayout.Button ("Create Terrain")) {
			createTerrain.BuildTerrain ();
		}

		if (GUILayout.Button ("Delete Terrain")) {
			createTerrain.DeleteTerrain ();
		}
	}
}
#endif
