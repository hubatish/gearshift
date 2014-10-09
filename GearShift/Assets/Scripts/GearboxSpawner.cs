using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearboxSpawner : MonoBehaviour {
	public GameObject[] gearList;
	//ArrayList createdGears = new ArrayList();
	public Vector3[] spawnpoints = new Vector3[4];
	List<GameObject> createdGears = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			createdGears.Add(spawnGear(i));
		}
	}

	void Update(){
		for (int i = 3; i >= 0; i--) {
			if(createdGears[i].layer == LayerMask.NameToLayer ("Default")){
				createdGears[i] = spawnGear(i);
			}
		}
	}

	GameObject spawnGear(int i){
		GameObject newGear = Instantiate (gearList [0], spawnpoints [i], transform.rotation) as GameObject;
		return newGear;
	}
		
}
