using UnityEngine;
using System.Collections;

//game object will destroy itself x seconds after creation
public class self_destruct : MonoBehaviour {
	public float secondsToDestruction;
	float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > secondsToDestruction) {
			Destroy (gameObject);
		}
	}
}
