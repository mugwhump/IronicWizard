using UnityEngine;
using System.Collections;

//This class has stuff the player does :DDDD
public class Player : MonoBehaviour {
	public Collider bonkCollider; //hitbox for bonking stuff



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (bonkCollider, transform.position + (transform.forward * 2.5f), transform.rotation);
		}
	}
}
