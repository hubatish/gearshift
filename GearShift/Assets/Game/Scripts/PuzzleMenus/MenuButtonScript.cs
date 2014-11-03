using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {
	public Texture2D buttonNeutral, buttonHover, buttonClicked;
	
	void OnMouseEnter(){
		guiTexture.texture = buttonHover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = buttonNeutral;
	}
	
	void OnMouseDown(){
		guiTexture.texture = buttonClicked;
		Application.LoadLevel (0);
	}
}
