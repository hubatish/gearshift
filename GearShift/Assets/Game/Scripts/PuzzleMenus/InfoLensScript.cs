using UnityEngine;
using System.Collections;

public class InfoLensScript : MonoBehaviour {
	public Texture2D buttonNeutral, buttonHover, buttonClicked;
	public GameObject infoScreen;

	void OnMouseEnter(){
		guiTexture.texture = buttonHover;
	}

	void OnMouseExit(){
		guiTexture.texture = buttonNeutral;
	}

	void OnMouseDown(){
		guiTexture.texture = buttonClicked;
		infoScreen.SetActive (!infoScreen.activeSelf);
		// Pull up info panel
	}
}
