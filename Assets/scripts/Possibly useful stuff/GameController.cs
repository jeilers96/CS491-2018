using UnityEngine;
using System;

/// <summary>
/// The game controller. Static class is always available to any object in any scene. To keep settings persistent, 
/// the game controller saves the data from settings into player prefs.
/// </summary>
public static class GameController{

	/// <summary>
	/// Should the tooltips display over ui elements
	/// </summary>
	public static bool tooltip = true;
	/// <summary>
	/// Should there be background music playing
	/// </summary>
	public static bool music = true;
	/// <summary>
	/// Should there be sound FX during gameplay
	/// </summary>
	public static bool sounds = true;

	/// <summary>
	/// Deletes all the playerprefs related to settings.
	/// </summary>
	public static void DeleteAll(){
		PlayerPrefs.DeleteKey ("tooltip");
		PlayerPrefs.DeleteKey ("music");
		PlayerPrefs.DeleteKey ("sound");
	}

	//saves data out to player prefs
	public static void Save(int tool, int mus, int sound){
		PlayerPrefs.SetInt("tooltip", tool);
		PlayerPrefs.SetInt ("music", mus);
		PlayerPrefs.SetInt ("sound", sound);

	}

	//loads data out of player prefs
	public static void Load(){
		if (PlayerPrefs.HasKey ("tooltip")) {
			if (PlayerPrefs.GetInt ("tooltip") == 1) {
				tooltip = true;
			} else {
				tooltip = false;
			}
		} else {
			tooltip = true;
		}
		if (PlayerPrefs.HasKey ("music")) {
			if (PlayerPrefs.GetInt ("music") == 1) {
				music = true;
			} else {
				music = false;
			}
		} else {
			music = true;
		}
		if (PlayerPrefs.HasKey ("sound")) {
			if (PlayerPrefs.GetInt ("sound") == 1) {
				sounds = true;
			} else {
				sounds = false;
			}
		} else {
			sounds = true;
		}
	}
}

