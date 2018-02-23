using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Creates the object selected by the user and subtracts the cost to buy it.
/// </summary>
public class CreateBuyableObject : MonoBehaviour {

	/// <summary>
	/// Creates the given usable object and puts it centered on the screen
	/// </summary>
	/// <param name="prefab">Parameter to pass.</param>
	public void CreateUsableObject(GameObject prefab){
		GameObject clone = Instantiate (prefab);
		float zPos = clone.transform.position.z;
		LevelController.instance.CurrentObjectCount++;
		//make it a child of TheLevelObjects and center it to where the camera is
		clone.transform.SetParent (LevelController.instance.theLevelObjects.transform, false);
		Camera cam = Camera.allCameras[0];
		Vector2 center = cam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f));
		Vector3 centerWithZ = new Vector3 (center.x, center.y, zPos);
		clone.transform.position = centerWithZ;

		//set selected object to this new object so that way the showSelected will highlight it
		LevelController.instance.selectedObject = clone;
		SubtractAvailableMoney (clone);
		LevelController.instance.allBoughtObjects.Add (clone);
	}

	/// <summary>
	/// Subtracts the cost of the bought item from the available money.
	/// </summary>
	/// /// <param name="prefab">Parameter to pass.</param>
	private void SubtractAvailableMoney(GameObject prefab){
		switch (prefab.tag) {
		case "Conveyor":
			LevelController.instance.startingMoney -= LevelController.instance.ConveyorCost;
			break;
		case "Trampoline":
			LevelController.instance.startingMoney -= LevelController.instance.TrampolineCost;
			break;
		case "Slide":
			LevelController.instance.startingMoney -= LevelController.instance.SlideCost;
			break;
		case "Fan":
			LevelController.instance.startingMoney -= LevelController.instance.FanCost;
			break;
		case "Glue":
			LevelController.instance.startingMoney -= LevelController.instance.GlueCost;
			break;
		case "Magnet":
			LevelController.instance.startingMoney -= LevelController.instance.MagnetCost;
			break;
		case "Funnel":
			LevelController.instance.startingMoney -= LevelController.instance.FunnelCost;
			break;
		default:
			print ("Error in CreateBuyableObject.cs: " + this.tag + " - must have an appropriate tag. e.g. Conveyor");
			break;
		}
	}
}
