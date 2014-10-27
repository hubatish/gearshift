using UnityEngine;
using System.Collections;

public class GearCounterGUI : MonoBehaviour {
	public GUIStyle style;
	public Rect counterPos = new Rect(10, Screen.height/6, Screen.width-20, 100);
	GearShift.GearCounter gearCounter;
	string destroyText; 
	// Use this for initialization
	void Start () {
		// Load the Controller's Gear Counter Script
		GameObject gearSpawner = GameObject.Find ("GearCounter");
		gearCounter = gearSpawner.GetComponent<GearShift.GearCounter> ();
		style.normal.textColor = Color.white;
		style.fontSize = 18;

	}
	
	// Update is called once per frame
	void Update () {
		destroyText = "Gears Destroyed: " + gearCounter.gearsRemoved.ToString(); // not destroyed though?
		counterPos = new Rect(10, Screen.height/6, Screen.width-20, 100);
	}

	void OnGUI(){
		GUI.Label (counterPos, destroyText, style);
	}
}
