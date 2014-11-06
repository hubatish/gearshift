using UnityEngine;
using System.Collections;

public class InfoLensScript : MonoBehaviour {
	public Texture2D buttonNeutral, buttonHover, buttonClicked;
	public GameObject infoScreen;

	void OnEnable(){
		renderer.material.mainTexture = buttonNeutral;
	}

	void OnMouseEnter(){
		renderer.material.mainTexture = buttonHover;
	}

	void OnMouseExit(){
		renderer.material.mainTexture = buttonNeutral;
	}

	void OnMouseDown(){
		renderer.material.mainTexture = buttonClicked;
		infoScreen.SetActive (!infoScreen.activeSelf);
		// Pull up info panel
	}
}
