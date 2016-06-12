using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

	void Awake() {
		//Debug.Log("Music player Awake ID: " + GetInstanceID());
		//if instance of MusicPlayer exists, destroy this one
		if (instance != null) {
			Destroy(gameObject);
			//Debug.Log("Duplicate music player self-destruct!");
		} else {
			//set instance so the next creation gets destroyed
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start() {
		//get the AudioSource and play the music
		AudioSource audio = GetComponent<AudioSource>();
		if (audio) {
			audio.Play();
			//Debug.Log("Audio is playing");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
