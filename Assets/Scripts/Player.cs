using UnityEngine;
using System.Collections;

//This class has stuff the player does :DDDD
public class Player : MonoBehaviour {
	public Collider bonkCollider; //hitbox for bonking stuff
    public ParticleSystem mouseClickParticle; //the mousey-clicky particle effect
    public Collider wand; //Where the magic comes from!

	// Use this for initialization
	void Start () {
		//create wand
		Instantiate (wand, transform.position + new Vector3(-4.15f, 2.1f, 10.3f), Quaternion.FromToRotation(Vector3.up, transform.forward + new Vector3(0,194.3f,0)));
			} 

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (bonkCollider, transform.position + (transform.forward * 3f), transform.rotation);
            Instantiate (mouseClickParticle, (wand.transform.position + wand.transform.up), transform.rotation);
		}
	}
}
