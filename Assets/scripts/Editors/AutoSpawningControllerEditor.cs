#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Spawning Controller editor. Used to easily generate infinite or random spawned objects from a list of a few objects.
/// </summary>
[CustomEditor(typeof(AutoSpawningController))]
public class AutoSpawningControllerEditor : Editor{
	/// <summary>
	/// Raises the inspector GU event. Updates the unity inspector to have an options list for direction and a button for creating and deleting scaffold.
	/// </summary>
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		AutoSpawningController autoSpawningController = (AutoSpawningController)target;

		if (GUILayout.Button ("Auto Generate")) {
			autoSpawningController.AutoGenerate();
		}
	}
}
#endif
