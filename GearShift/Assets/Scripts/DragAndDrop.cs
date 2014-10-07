using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {
	public GameObject baseGear;
	public bool isMoveable = true;
	private Vector3 pointOnScreen;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Needed for later "finalizing" gear placement
	void setMoveable(bool flag){
		isMoveable = flag;
	}

	void OnMouseDown()
	{ 
		pointOnScreen = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointOnScreen.z));
	}
	
	void OnMouseDrag() 
	{  
		if (isMoveable) {
			Vector3 currentPointOnScreen = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, pointOnScreen.z);
			Vector3 newGearPosition = Camera.main.ScreenToWorldPoint (currentPointOnScreen) + offset;
			transform.position = newGearPosition;
		}
	}
}
