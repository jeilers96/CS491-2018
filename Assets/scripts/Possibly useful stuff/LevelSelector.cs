using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Level selector is used to dynamically update the level selection screen with the appropriate stars earned by the player and 
/// shows if they have unlocked the level or not.
/// </summary>
public class LevelSelector : MonoBehaviour {

	public Image thumbnail;
	public Text levelText;
	public int unlocked;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
}
