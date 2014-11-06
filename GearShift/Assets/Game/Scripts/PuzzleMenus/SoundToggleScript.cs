using UnityEngine;
using System.Collections;

public class SoundToggleScript : MonoBehaviour {
	public Texture2D soundOn, soundOff;
	public GameObject audioObj;
	void Start(){

	}
	
	void OnMouseDown(){
		if (renderer.material.mainTexture.name.Contains ("SoundON")) {
			renderer.material.mainTexture = soundOff;
			audioObj.audio.Pause();
		} else if (renderer.material.mainTexture.name.Contains ("SoundOFF")) {
			renderer.material.mainTexture = soundOn;
			audioObj.audio.Play();
		} else {
			Debug.LogError("Couldn't toggle sound!");
		}
	}
}
