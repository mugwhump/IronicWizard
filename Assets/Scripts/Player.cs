using UnityEngine;
using System.Collections;

//This class has stuff the player does :DDDD
public class Player : MonoBehaviour {
	public Collider bonk_collider; //hitbox for bonking stuff



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (bonk_collider, transform.position + (transform.forward * 2), transform.rotation);
		}
	}
}
