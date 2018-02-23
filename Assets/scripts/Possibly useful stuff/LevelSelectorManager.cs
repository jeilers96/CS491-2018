using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the level selection scene by generating levels based off of player prefs and builds them using LevelSelector objects
/// </summary>
public class LevelSelectorManager : MonoBehaviour {

	[System.Serializable]
	public class Level{
		public string levelText;
		public int unlocked;
		public bool isInteractable;
		public Sprite thumbnail;
	}

	public List<Level> levelList;
	public GameObject button;
	public Transform spacer;

	// Use this for initialization
	void Start () {
		if (button != null) {
			FillList ();
		}
	}

	/// <summary>
	/// Takes the levelList and levelselector objects and associates them with a button that will load the correct scene.
	/// </summary>
	void FillList(){
		foreach (var level in levelList) {
			GameObject newButton = Instantiate (button) as GameObject;
			newButton.transform.SetParent (spacer, false);
			LevelSelector levelSelect = newButton.GetComponent<LevelSelector> ();
			levelSelect.levelText.text = level.levelText;
			levelSelect.thumbnail.sprite = level.thumbnail;
			if (PlayerPrefs.GetInt (levelSelect.levelText.text) == 1) {
				level.unlocked = 1;
				level.isInteractable = true;
			}

			levelSelect.GetComponent<Button> ().onClick.AddListener (() => LoadLevel (levelSelect.levelText.text));

			if(PlayerPrefs.HasKey(levelSelect.levelText.text + " stars")){
				int stars = PlayerPrefs.GetInt(levelSelect.levelText.text + " stars");
				switch (stars) {
				case 1:
					levelSelect.star1.transform.GetChild (1).gameObject.SetActive (true);
					break;
				case 2:
					levelSelect.star1.transform.GetChild (1).gameObject.SetActive (true);
					levelSelect.star2.transform.GetChild (1).gameObject.SetActive (true);
					break;
				case 3:
					levelSelect.star1.transform.GetChild (1).gameObject.SetActive (true);
					levelSelect.star2.transform.GetChild (1).gameObject.SetActive (true);
					levelSelect.star3.transform.GetChild (1).gameObject.SetActive (true);
					break;
				default:
					break;
				}
			}

			levelSelect.unlocked = level.unlocked;
			levelSelect.GetComponent<Button> ().interactable = level.isInteractable;

		}
		SaveAll ();
	}

	/// <summary>
	/// Saves all the data needed to ensure the levels remain unlocked.
	/// </summary>
	void SaveAll(){
		if (!PlayerPrefs.HasKey ("Level 1")) {
			GameObject[] allButtons = GameObject.FindGameObjectsWithTag ("LevelButton");
			foreach (GameObject button in allButtons) {
				LevelSelector levelSelect = button.GetComponent<LevelSelector> ();
				PlayerPrefs.SetInt (levelSelect.levelText.text, levelSelect.unlocked);
			}
		}
		PlayerPrefs.Save ();
	}

	/// <summary>
	/// delete all keys related to the level selector, including stars and unlocked levels, can't delete
	/// all PlayerPrefs because it would remove settings
	/// </summary>
	public void DeleteAll(){
		for (int i = 1; i < 15; i++) {
			PlayerPrefs.DeleteKey ("Level " + i + " stars");
			PlayerPrefs.DeleteKey ("Level " + i);
		}
		PlayerPrefs.Save ();
	}

	/// <summary>
	/// WARNING: Removes all player prefs for the game. Should only be used to completely reset everything for accurate testing.
	/// </summary>
	public void DeleteAllPlayerPrefs(){
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}

	/// <summary>
	/// Loads the level with the string name.
	/// </summary>
	/// <param name="value">Value.</param>
	void LoadLevel(string value){
		SceneManager.LoadScene(value);
	}

	/// <summary>
	/// Returns the stars earned for the given level index, or -1 if the index is out of bounds or the level isn't yet unlocked.
	/// </summary>
	/// <returns>The earned for level.</returns>
	/// <param name="level">Level.</param>
	public int starsEarnedForLevel(int level){
		if (PlayerPrefs.HasKey ("Level " + (level).ToString () + " stars")) {
			return PlayerPrefs.GetInt ("Level " + (level).ToString () + " stars");
		} else {
			return -1;
		}
	}

	/// <summary>
	/// Sets the number of stars for the given level.
	/// </summary>
	/// <param name="level">Level.</param>
	/// <param name="stars">Stars.</param>
	public void setStarsForLevel(int level, int stars){
		PlayerPrefs.SetInt("Level " + (level).ToString() + " stars", stars);
		PlayerPrefs.Save ();
	}

	/// <summary>
	/// Unlocks the next level, given the index of that next level.
	/// </summary>
	/// <param name="levelToUnlock">Level to unlock.</param>
	public void unlockNextLevel(int levelToUnlock){
		if (levelToUnlock > 0) {
			PlayerPrefs.SetInt ("Level " + levelToUnlock.ToString (), 1);
			//set scores key so that way the stars can be activated accordingly
			PlayerPrefs.SetInt("Level " + (levelToUnlock).ToString() + " stars", 0);
		}
		PlayerPrefs.Save ();
	}

	public void unlockAllLevels(){
		for (int i = 1; i <= 15; i++) {
			PlayerPrefs.SetInt ("Level " + i, 1);
			PlayerPrefs.SetInt ("Level " + i + " stars", 0);
		}
		PlayerPrefs.Save ();
	}
}
