#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Generate Level Grid Editor to enable building a simple grid level with the Inspector
/// </summary>
[CustomEditor(typeof(GenerateLevelGrid))]
public class GenerateLevelGridEditor : Editor{
	/// <summary>
	/// Raises the inspector GU event. Updates the unity inspector to have an options list for direction and a button for creating and deleting scaffold.
	/// </summary>
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		GenerateLevelGrid generateLevelGrid = (GenerateLevelGrid)target;

		if (GUILayout.Button ("Fill Grid Array")) {
			generateLevelGrid.FillGenericGrid ();
		}

		if (GUILayout.Button ("Build Grid Objects")) {
			generateLevelGrid.BuildGrid ();
		}

		if (GUILayout.Button ("Delete Grid Objects")) {
			generateLevelGrid.DeleteGrid ();
		}

		if (GUILayout.Button ("Reset Array Completely")) {
			generateLevelGrid.ResetGridArray ();
		}
	}
}
#endif