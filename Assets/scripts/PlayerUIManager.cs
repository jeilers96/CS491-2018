using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {

	public static PlayerUIManager instance;
	public GameObject CharacterCell;

	private LevelManager levelManager; 
	private List<GameObject> player1Cells;
	private List<GameObject> player2Cells;
	public List<Sprite> characterThumbnails;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		levelManager = LevelManager.instance;

		Sprite thumbnail;
		thumbnail = Resources.Load<Sprite> ("doubleJumpManThumbnail");
		if (thumbnail != null) {
			characterThumbnails.Add (thumbnail);
		} 

		thumbnail = Resources.Load<Sprite> ("swimmerThumbnail");
		if (thumbnail != null) {
			characterThumbnails.Add (thumbnail);
		}

		thumbnail = Resources.Load<Sprite> ("strongmanThumbnail");
		if (thumbnail != null) {
			characterThumbnails.Add (thumbnail);
		}

		thumbnail = Resources.Load<Sprite> ("sprinterThumbnail");
		if (thumbnail != null) {
			characterThumbnails.Add (thumbnail);
		}

		GeneratePlayerUI ();

		UpdatePlayer1UI ();
		UpdatePlayer2UI ();
	}

	// Update is called once per frame
	void Update () {

	}

	private void GeneratePlayerUI(){
		Transform player1UI = transform.GetChild (0);
		if (player1UI != null) {
			Transform player1Characters = player1UI.GetChild (0);
			if (player1Characters != null) {
				foreach (GameObject character in levelManager.playerOneCharacters) {
					switch (character.name) {
					case "doubleJumpCharacter":
						CreatePlayerCell (player1Characters, 0);
						break;
					case "swimmerCharacter":
						CreatePlayerCell (player1Characters, 1);
						break;
					case "strongmanCharacter":
						CreatePlayerCell (player1Characters, 2);
						break;
					case "sprinterCharacter":
						CreatePlayerCell (player1Characters, 3);
						break;
					default:
						break;
					}
				}
			}
		}

		Transform player2UI = transform.GetChild (1);
		if (player2UI != null) {
			Transform player2Characters = player2UI.GetChild (0);
			if (player2Characters != null) {
				foreach (GameObject character in levelManager.playerTwoCharacters) {
					switch (character.name) {
					case "doubleJumpCharacter":
						CreatePlayerCell (player2Characters, 0);
						break;
					case "swimmerCharacter":
						CreatePlayerCell (player2Characters, 1);
						break;
					case "strongmanCharacter":
						CreatePlayerCell (player2Characters, 2);
						break;
					case "sprinterCharacter":
						CreatePlayerCell (player2Characters, 3);
						break;
					default:
						break;
					}
				}
			}
		}
	}

	public void UpdatePlayer1UI(){
		Transform player1UI = transform.GetChild (0);
		if (player1UI != null) {
			Transform player1Characters = player1UI.GetChild (0);
			if (player1Characters != null) {
				for (int i = 0; i < player1Characters.childCount; i++) {
					Transform player1Cell = player1Characters.GetChild (i);
					if (player1Cell != null) {
						Transform characterTransform = player1Cell.GetChild (0);
						Transform borderTransform = player1Cell.GetChild (1);
						if (levelManager.PlayerOneIndex == i) {
							RemoveTransparency (characterTransform);
							RemoveTransparency (borderTransform);
						} else {
							SetTransparent (characterTransform);
							SetTransparent (borderTransform);
						}
					}
				}
			}
		}
	}

	public void UpdatePlayer2UI(){
		Transform player2UI = transform.GetChild (1);
		if (player2UI != null) {
			Transform player2Characters = player2UI.GetChild (0);
			if (player2Characters != null) {
				for (int i = 0; i < player2Characters.childCount; i++) {
					Transform player2Cell = player2Characters.GetChild (i);
					if (player2Cell != null) {
						Transform characterTransform = player2Cell.GetChild (0);
						Transform borderTransform = player2Cell.GetChild (1);
						if (levelManager.PlayerTwoIndex == i) {
							RemoveTransparency (characterTransform);
							RemoveTransparency (borderTransform);
						} else {
							SetTransparent (characterTransform);
							SetTransparent (borderTransform);
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// Sets the image transparency. Amount of Alpha between 0 and 1, 0 being fully transparent.
	/// </summary>
	/// <param name="amount">Amount.</param>
	/// <param name="transform">Transform.</param>
	private void SetImageTransparency(float amount, Transform transform){
		if (transform != null) {
			Image characterImage = transform.GetComponent<Image> ();
			Color originalColor = characterImage.color;
			originalColor.a = amount;
			characterImage.color = originalColor;
		}
	}

	private void SetTransparent(Transform transform){
		SetImageTransparency (0.25f, transform);
	}

	private void RemoveTransparency(Transform transform){
		SetImageTransparency (1.0f, transform);
	}

	private void CreatePlayerCell(Transform playerUI, int index){
		GameObject characterCell = Instantiate (CharacterCell, playerUI) as GameObject;
		characterCell.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		characterCell.transform.SetParent (playerUI);
		Transform playerCellTransform = characterCell.transform.GetChild (0);
		if (playerCellTransform != null) {
			playerCellTransform.GetComponent<Image> ().sprite = characterThumbnails [index];
		}
	}
}
