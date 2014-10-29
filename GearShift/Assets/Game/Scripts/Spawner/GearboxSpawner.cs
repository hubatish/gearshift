using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Spawns gears and puts them in the toolbox at spawnpoints array locations
/// </summary>
public class GearboxSpawner : MonoBehaviour {
	public GameObject[] gearList;
	public int[] spawnProbability;
	public Vector3[] spawnpoints = new Vector3[5];
	List<GameObject> probabilitySpawnList = new List<GameObject>();
	List<GameObject> createdGears = new List<GameObject>();

    protected int numGearsSpawned = 0;


	// Use this for initialization
	void Start () {
		// Fill out the spawn list based on probability
		for (int gearIndex = 0; gearIndex < gearList.Length; gearIndex++) {
			for(int probIndex = 0; probIndex < spawnProbability[gearIndex]; probIndex++){
				probabilitySpawnList.Add(gearList[gearIndex]);
			}
		}

		for (int i = 0; i < 5; i++) {
			createdGears.Add(spawnGear(i));
		}
	}

	void Update(){
		for (int i = 4; i >= 0; i--) {
			if(createdGears[i].layer == LayerMask.NameToLayer ("Default")){
				createdGears[i] = spawnGear(i);
			}
		}
	}

	GameObject spawnGear(int i){
		GameObject randomGear = getRandomGear();
		GameObject newGear = Instantiate (randomGear, spawnpoints [i], Quaternion.identity) as GameObject;
        //name the gear for helpful debugging ness
        numGearsSpawned += 1;
        newGear.name += numGearsSpawned.ToString();
		return newGear;
	}

	GameObject getRandomGear(){
		var index = Random.Range(0, probabilitySpawnList.Count);
		return probabilitySpawnList[index];
	}
		
}
