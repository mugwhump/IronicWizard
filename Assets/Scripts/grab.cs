using UnityEngine;
using System.Collections;

public class grab : MonoBehaviour {
	
	void OnTriggerEnter (Collider other) {
		Rigidbody rb = other.GetComponent<Rigidbody>();
		if ( rb ) {
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Player> ().SetGrabbed (rb);
			Debug.Log ("I graeb u");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
