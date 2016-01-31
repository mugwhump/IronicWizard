using UnityEngine;
using System.Collections;

public enum soundType {glass, metal, box};

//plays object's sound type upon drastic movement
public class play_audio_movement : MonoBehaviour {
	public soundType type;

	AudioSource source;

	// Use this for initialization
	void Start () {
		source = gameObject.AddComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision other) {
		Debug.Log ("Play sound!");
		AudioClip clip = GameObject.FindGameObjectWithTag ("SoundController").GetComponent<SoundController> ().GetSound (type);
		source.clip = clip;
		source.Play ();
	}
}
