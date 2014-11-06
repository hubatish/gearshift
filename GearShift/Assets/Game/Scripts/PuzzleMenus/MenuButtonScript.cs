using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {
	public Texture2D buttonNeutral, buttonHover, buttonClicked;
	
	void OnMouseEnter(){
		renderer.material.mainTexture = buttonHover;
	}
	
	void OnMouseExit(){
		renderer.material.mainTexture = buttonNeutral;
	}
	
	void OnMouseDown(){
		renderer.material.mainTexture = buttonClicked;
		Application.LoadLevel (0);
	}
}
