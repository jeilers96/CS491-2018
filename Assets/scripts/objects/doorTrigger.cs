using UnityEngine;

public class doorTrigger : MonoBehaviour {

	public door door;
	public bool StayOpen = false;
	public int btnsNeeded = 1;


	public int btnsPressedCount = 0;

	public void Toggle(bool state) {
		if(state) {
			btnsPressedCount++;
			if (btnsPressedCount == btnsNeeded) {
				door.DoorOpens ();
			}
		} else{
			btnsPressedCount--;
			if (!StayOpen) {
				door.DoorCloses ();
			}
		}
	}

	void onTriggerEnter2D(Collider2D other) {
		if(other.tag=="player") {
			door.DoorOpens ();
		}
	}
	
	void onTriggerExit2D(Collider2D other) {
		if(other.tag=="player" && !StayOpen) {
			door.DoorCloses ();
		}
	}
}
