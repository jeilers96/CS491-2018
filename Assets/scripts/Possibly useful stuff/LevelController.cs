using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

/* Should be attached to 'Level Manager' which should exist on every level to manage that specific level by 
 * setting instance variables that will be referenced by all other objects */
public class LevelController : MonoBehaviour {

	public class AllPackages{
		public List<GameObject> packages;
	}

	/* ==================== BEGIN MEMBER VARIABLES ====================== */

	//static instance used for other scripts to reference
	public static LevelController instance;

	/* =============== CORE LEVEL OBJECTS ================= */

	/// <summary>
	/// The main canvas which should be the parent for all spawned objects.
	/// </summary>
	public GameObject canvas;

	/// <summary>
	/// The GameObject that holds all the objects besides the ui canvas
	/// </summary>
	public GameObject theLevelObjects;

	/// <summary>
	/// The canvas overlapping theLevelObjects in order for ui elements to overlap the game objects such as the 
	/// show selected object and package damage indicator to work correctly.
	/// </summary>
	public GameObject levelCanvas;

	/// <summary>
	/// The object panel to update and modify objects.
	/// </summary>
	public GameObject objectPanel;

	/// <summary>
	/// The summary canvas is the canvas that overlays the camera with a summary of how the player did for this level,
	/// and should only be set active after all packages have been destroyed.
	/// </summary>
	public GameObject summaryCanvas;

	/// <summary>
	/// A reference to all of the objects bought by the player. Used to recreate a saved level.
	/// </summary>
	public List<GameObject> allBoughtObjects;

	/// <summary>
	/// The current object selected, this object can then be modified by the object panel.
	/// </summary>
	public GameObject selectedObject = null;

	/* =============== STARS ================= */

	/// <summary>
	/// The stars earned for this level, determined by the 3 tracked progresses: money earned, packages delivered, 
	/// and number of objects used.
	/// </summary>	
	public int starsEarned = 0;

	/// <summary>
	/// The money needed in order to earn 1 star for this level.
	/// </summary>
	public int moneyFor1Star = 1500;

	/// <summary>
	/// The number of packages delivered to earn 1 star for this level.
	/// </summary>
	public int packagesFor1Star = 25;

	/// <summary>
	/// The max number of objects a player can use before losing 1 star for this level.
	/// </summary>
	public int maxObjectsUsedFor1Star = 15;

	private int currentObjectCount;
	/// <summary>
	/// The current amount of bought objects on the level, compared against maxObjectsUsedFor1Star.
	/// </summary>
	public int CurrentObjectCount{
		get{ return currentObjectCount; }
		set{ currentObjectCount = value; }
	}

	/// <summary>
	/// The index of the level, starting at 1. e.g. level 1's level should be 1.
	/// </summary>
	public int level = 0;

	/* =============== MONEY ================= */

	/// <summary>
	/// The amount of money the player gets when the level first loads and is all the money they can spend on objects. 
	/// Should be updated whenever the user buys or sells something.
	/// </summary>
	public int startingMoney = 4000;

	private int currentMoney;
	/// <summary>
	/// How much money the player has earned or lost as they play the level.
	/// </summary>
	public int CurrentMoney{
		get{ return currentMoney; }
		set{ currentMoney = value; }
	}

	/* =============== OBJECT COSTS ================= */

	private int conveyorCost = 450;
	/// <summary>
	/// The cost to buy one conveyor.
	/// </summary>
	public int ConveyorCost{
		get{ return conveyorCost; }
		set{ conveyorCost = value; }
	}

	private int trampolineCost = 350;
	/// <summary>
	/// The cost to buy one trampoline.
	/// </summary>
	public int TrampolineCost{
		get{ return trampolineCost; }
		set{ trampolineCost = value; }
	}

	private int slideCost = 275;
	/// <summary>
	/// The cost to buy one slide.
	/// </summary>
	public int SlideCost{
		get{ return slideCost; }
		set{ slideCost = value; }
	}

	private int funnelCost = 400;
	/// <summary>
	/// The cost to buy one funnel.
	/// </summary>
	public int FunnelCost{
		get{ return funnelCost; }
		set{ funnelCost = value; }
	}

	private int fanCost = 300;
	/// <summary>
	/// The cost to buy one fan.
	/// </summary>
	public int FanCost{
		get{ return fanCost; }
		set{ fanCost = value; }
	}

	private int glueCost = 350;
	/// <summary>
	/// Thec cost to buy one glue.
	/// </summary>
	public int GlueCost{
		get{ return glueCost; }
		set{ glueCost = value; }
	}

	private int magnetCost = 300;
	/// <summary>
	/// The cost to buy one magnet.
	/// </summary>
	public int MagnetCost{
		get{ return magnetCost; }
		set{ magnetCost = value; }
	}

	/* =============== PACKAGES ================= */

	/// <summary>
	/// The amount of money a regular package cost.
	/// </summary>
	public float packageWorth = 150.0f;

	private float timeCounter;
	/// <summary>
	/// The time that the game has been played, used for spawning packages on time
	/// </summary>
	public float TimeCounter{
		get{ return timeCounter; }
		set{ timeCounter = value; }
	}

	/* =============== PACKAGE TRACKING ================= */

	private int successfulPackages;
	/// <summary>
	/// The count of packages successfully delivered.
	/// </summary>
	public int SuccessfulPackages{
		get{ return successfulPackages; }
		set{ successfulPackages = value; }
	}

	private int failurePackages;
	/// <summary>
	/// The count of packages that were not successfully delivered (destroyed or dropped into the wrong truck)
	/// </summary>
	public int FailurePackages{
		get{ return failurePackages; }
		set{ failurePackages = value; }
	}

	private int numPackagesLeft;
	/// <summary>
	/// The number of packages left to spawn before its the end of the level.
	/// </summary>
	public int NumPackagesLeft{
		get{ return numPackagesLeft; }
		set{ numPackagesLeft = value; }
	}

	/* =============== GAME SPEED ================= */

	private float pauseGameSpeed = 0.0f;
	/// <summary>
	/// The paused game speed when the level starts and when the pause button is hit.
	/// </summary>
	public float PauseGameSpeed{
		get{ return pauseGameSpeed; }
		set{ pauseGameSpeed = value; }
	}

	private float regularGameSpeed = 1.0f;
	/// <summary>
	/// The regular game speed when the play button is hit.
	/// </summary>
	public float RegularGameSpeed{
		get{ return regularGameSpeed; }
		set{ regularGameSpeed = value; }
	}

	private float fastGameSpeed = 2.0f;
	/// <summary>
	/// The fast game speed when the fast play button is hit.
	/// </summary>
	public float FastGameSpeed{
		get{ return fastGameSpeed; }
		set{ fastGameSpeed = value; }
	}

	private float superGameSpeed = 3.0f;
	/// <summary>
	/// The super fast game speed when the super fast button is hit.
	/// </summary>
	public float SuperGameSpeed{
		get{ return superGameSpeed; }
		set{ superGameSpeed = value; }
	}

	private bool isPaused = true;
	/// <summary>
	/// The bool to keep track of whether or not the level is paused.
	/// </summary>
	public bool IsPaused{
		get{ return isPaused; }
		set{ isPaused = value; }
	}

	/* ================== END OF MEMBER VARIABLES ==================== */

	//called before any start methods
	void Awake () {
		instance = this;
	}

	void Start(){
		if (objectPanel != null) {
			objectPanel.SetActive (false);
		}
	}

	/// <summary>
	/// Called when this script will be destroyed, used to check how many stars were earned.
	/// </summary>
	void OnDestroy(){
		int previousStars = gameObject.GetComponent<LevelSelectorManager> ().starsEarnedForLevel (level);
		if (starsEarned > previousStars) {
			gameObject.GetComponent<LevelSelectorManager> ().setStarsForLevel (level, starsEarned);
			if (starsEarned >= 2) {
				gameObject.GetComponent<LevelSelectorManager> ().unlockNextLevel (level + 1);
			}
		}
	}

}