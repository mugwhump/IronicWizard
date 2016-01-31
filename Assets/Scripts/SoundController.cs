using UnityEngine;
using System.Collections;

//Contains arrays of sfx for similar objects
public class SoundController : MonoBehaviour {
	public AudioClip[] glass;
	public AudioClip[] metal;
	public AudioClip[] box;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	AudioClip RandomSound(AudioClip[] arr) {
		int ind = Mathf.FloorToInt(Random.Range(0, arr.Length));
		return arr[ind];
	}

	public AudioClip GetSound(soundType type){
		switch (type) {
		case soundType.glass:
			return RandomSound (glass);
			break;
		case soundType.metal:
			return RandomSound (metal);
			break;
		case soundType.box:
			return RandomSound (box);
			break;
		}
		return null;
	}
			
}
