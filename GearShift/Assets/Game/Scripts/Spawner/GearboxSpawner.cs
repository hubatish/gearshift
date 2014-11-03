using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GearShift;

/// <summary>
/// Spawns gears and puts them in the toolbox at spawnpoints array locations
/// </summary>
public class GearboxSpawner : MonoBehaviour {
	public GameObject[] gearList; // List of gears to be spawned
	public int[] spawnProbability; // Probability attached to each gear
	public Vector3[] spawnpoints = new Vector3[5]; // Location of Spawn
	List<GameObject> probabilitySpawnList = new List<GameObject>(); // Full "pool" of gears that are chosen from
	List<GameObject> createdGears = new List<GameObject>(); // Gears currently in gearbox

	private int nullGearSpawnCount = 0;
	public int nullGearSpawnMax = 3;

    protected int numGearsSpawned = 0;

    public LayerController layers;

	// Use this for initialization
	void Start () {
		// Fill out the spawn list based on probability
		for (int gearIndex = 0; gearIndex < gearList.Length; gearIndex++) {
			for(int probIndex = 0; probIndex < spawnProbability[gearIndex]; probIndex++){
				probabilitySpawnList.Add(gearList[gearIndex]);
			}
		}

		// Spawn the initial number of gears
		for (int i = 0; i < spawnpoints.Length; i++) {
			createdGears.Add(spawnGear(i));
		}
	}

	void Update(){
		// Check if the # of gears needed are present in the gearbox
		for (int i = spawnpoints.Length - 1; i >= 0; i--) {
			if(createdGears[i].layer == LayerMask.NameToLayer ("Default")){
				createdGears[i] = spawnGear(i);
			}
		}
	}

	GameObject spawnGear(int i){
        //spawn a gear at the correct spawn point
		GameObject randomGear = getRandomGear();
        Vector3 adjustedSpawnPoint = new Vector3(spawnpoints[i].x, layers.getCurrentY(), spawnpoints[i].z);
		GameObject newGear = Instantiate (randomGear, adjustedSpawnPoint, Quaternion.identity) as GameObject;

        //name and organize the gears for debugging and layers
        numGearsSpawned += 1;
        newGear.name += numGearsSpawned.ToString();

        //temporarily put gears under myself
        newGear.transform.parent = transform;

		return newGear;
	}
	
	GameObject getRandomGear(){
		// To ensure that the gear spawns follow certain rules
		bool isValid = false;
		GameObject spawnGear = null;
		while (!isValid) {
			// Get the index of the spawn pool to pull the object from
			int index = Random.Range (0, probabilitySpawnList.Count);
			spawnGear = probabilitySpawnList [index];

			// Only allow for up to 3 "Null" gears per level
			if (spawnGear.name.Contains ("Null") && nullGearSpawnCount < nullGearSpawnMax) {
				nullGearSpawnCount++;
				//print ("Null Spawn: " + nullGearSpawnCount + "/" + nullGearSpawnMax);
				isValid = true;
			} else if (!spawnGear.name.Contains ("Null")) {
				isValid = true;
			}
		}
		return spawnGear;
	}
		
}
