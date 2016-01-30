using UnityEngine;
using System.Collections;

public class apply_force : MonoBehaviour {
	public float force;

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<Rigidbody> () != null) {
			other.GetComponent<Rigidbody> ().AddForce (transform.forward * force, ForceMode.Impulse);
		}
	}
		

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
