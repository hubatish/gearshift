using UnityEngine;
using System.Collections;

public class SoundToggleScript : MonoBehaviour {
	public Texture2D soundOn, soundOff;
	public GameObject audioObj;
	void Start(){

	}
	
	void OnMouseDown(){
		if (guiTexture.texture.name.Contains ("SoundON")) {
			guiTexture.texture = soundOff;
			audioObj.audio.Pause();
		} else if (guiTexture.texture.name.Contains ("SoundOFF")) {
			guiTexture.texture = soundOn;
			audioObj.audio.Play();
		} else {
			Debug.LogError("Couldn't toggle sound!");
		}
	}
}
