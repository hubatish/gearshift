using UnityEngine;
using System.Collections;

public class InfoLensScript : MonoBehaviour {
	public Texture2D buttonNeutral, buttonHover, buttonClicked;

	void OnMouseEnter(){
		guiTexture.texture = buttonHover;
	}

	void OnMouseExit(){
		guiTexture.texture = buttonNeutral;
	}

	void OnMouseDown(){
		guiTexture.texture = buttonClicked;
		// Pull up info panel
	}
}
