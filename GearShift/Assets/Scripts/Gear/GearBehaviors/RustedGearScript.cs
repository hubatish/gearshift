using UnityEngine;
using System.Collections;

public class RustedGearScript : MonoBehaviour {
	public int maxMovesTillDeletion = 5;
	private int prevTotalPlaced = 0;
	private bool turnChecked = false;
	GearShift.GearCounter gearCounter;

	void Start () {
		// Load the Controller's Gear Counter Script
		GameObject gearSpawner = GameObject.Find ("GearCounter");
		gearCounter = gearSpawner.GetComponent<GearShift.GearCounter> ();
	}

	void Update () {
		// Only check if this is also a placed object
		if (this.gameObject.layer == LayerMask.NameToLayer ("Default")) {
			// Make sure to only count a change on one gear added
			int placedGears = gearCounter.gearsPlaced;
			if (placedGears == prevTotalPlaced + 1){
				maxMovesTillDeletion--;
				turnChecked = false;

				// Break with increasing probability per turn passed
				breakProbability();
			}
			prevTotalPlaced = placedGears;
		}

	}

	void breakProbability(){
		int probDel = Random.Range(0, 101);
		// Only check once per object placed
		if (!turnChecked) {
			if (maxMovesTillDeletion ==  2) { // Only seems to delete here, check...
				//print ("Probability at turn 3 = " + probDel);
				if (probDel <= 15){
					gearCounter.removeGear();
					Destroy (this.gameObject);
				}
			} else if (maxMovesTillDeletion == 1) {
				//print ("Probability at turn 4 = " + probDel);
				if (probDel <= 40){
					gearCounter.removeGear();
					Destroy (this.gameObject);
				}
			} else if (maxMovesTillDeletion == 0) {
				//print ("Broken on turn 5");
				gearCounter.removeGear();
				Destroy (this.gameObject);
			}
			turnChecked = true;
		}
	}
	
}