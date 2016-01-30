using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

public class grab : MonoBehaviour {
	public Shader outlineShader;

	Player player;
	List<Rigidbody> items; //all items touching the box this frame
	
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
		if (rb && !rb.CompareTag ("Wand")) {
			items.Add (rb);
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
		items = new List<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		ChooseClosest ();
		items.Clear ();
	}

	//chooses closest object touching this to make the active object
	void ChooseClosest() {
		float minDist = 500f;
		Rigidbody closest = null;
		foreach(Rigidbody rb in items) {
			float dist = Vector3.Distance (transform.position, rb.transform.position);
			if (dist < minDist) {
				minDist = dist;
				closest = rb;
			}
		}
		if ( closest ) {
			player.SetActive (closest);
		}
	}
}
