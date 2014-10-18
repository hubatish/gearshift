using UnityEngine;
using System.Collections;

public class RustedGearScript : MonoBehaviour {
	public int movesTillDeletion = 5;
	public int numItemAdded = 0;
	private int prevTotalTagged = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Only check if this is also a placed object
		if (this.gameObject.layer == LayerMask.NameToLayer ("Default")) {
			// Check if another object was placed from the gearbox, edit later to only deal with locked gears
			int placedGears = GameObject.FindGameObjectsWithTag ("Placed Gear").Length;
			if (placedGears == prevTotalTagged + 1)
					numItemAdded++;

			//print("Number of items tagged: " + placedGears + ", num total added: " + numItemAdded );

			// Must be able to account for things like mass number of deletions
			prevTotalTagged = placedGears;
			if (numItemAdded == movesTillDeletion) {
					Destroy (this.gameObject);
			}
		}

	}


	
}