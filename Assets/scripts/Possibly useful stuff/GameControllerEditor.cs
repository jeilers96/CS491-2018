using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlAdjuster : MonoBehaviour {

	public Button mainMenu;
	public Toggle tooltip;
	public Toggle music;
	public Toggle sound;

	private int toolVal;
	private int musicVal;
	private int soundVal;

	public void UpdateSettings(Toggle toggle){

		switch (toggle.gameObject.name)
		{
		case "Tooltip":
			GameController.tooltip = toggle.isOn;
			break;
		case "Music":
			GameController.music = toggle.isOn;
			break;
		case "Sound":
			GameController.sounds = toggle.isOn;
			break;
		default:
			break;
		}
	}

	void Start(){
		//GameController.DeleteAll ();
		GameController.Load ();
		tooltip.isOn = GameController.tooltip;
		music.isOn = GameController.music;
		sound.isOn = GameController.sounds;
	}

	void OnDestroy(){
		if (tooltip.isOn) {
			toolVal = 1;
		} else {
			toolVal = 0;
		}
		if (music.isOn) {
			musicVal = 1;
		} else {
			musicVal = 0;
		}
		if (sound.isOn) {
			soundVal = 1;
		} else {
			soundVal = 0;
		}
		GameController.Save (toolVal, musicVal, soundVal);
	}
}
