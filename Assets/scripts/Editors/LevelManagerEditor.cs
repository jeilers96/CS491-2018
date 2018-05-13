#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor {

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		LevelManager levelManager = (LevelManager)target;

		if (GUILayout.Button ("Delete Save Data")) {
			levelManager.DeleteSaveData ();
		}

		if (GUILayout.Button ("Delete Game Save")) {
			levelManager.DeleteGameSaveData ();
		}
	}
}
#endif
