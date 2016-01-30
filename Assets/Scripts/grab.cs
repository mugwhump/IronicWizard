using UnityEngine;
using System.Collections;

public class grab : MonoBehaviour {
	public Shader outlineShader;

	Player player;
	
	void OnTriggerEnter (Collider other) {
//		Rigidbody rb = other.GetComponent<Rigidbody>();
//		if ( rb ) {
//			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Player> ().SetGrabbed (rb);
//		}
	}

	void OnTriggerStay (Collider other) {
		if (!player)
			return; //do nothing if player not instantiated yet
		Rigidbody rb = other.GetComponent<Rigidbody>();
		if ( rb && !rb.CompareTag("Wand")) {
			player.SetActive (rb);
		}
	}

	void OnTriggerExit (Collider other) {
		Rigidbody rb = other.GetComponent<Rigidbody>();
		if ( rb ) {
			player.SetInactive (rb);
		}
	}
		

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
