using UnityEngine;
using System.Collections;

//This class has stuff the player does :DDDD
public class Player : MonoBehaviour {
	public Collider bonkCollider; //hitbox for bonking stuff
	public Collider grabCollider; //hitbox for grabbing stuff
    public ParticleSystem mouseClickParticle; //the mousey-clicky particle effect
    public ParticleSystem grabParticle; //the grabby particle effect
    public Collider wand; //Where the magic comes from!

	public float grabforce; //force applied every frame to grabbed object

	Rigidbody grabbedObject = null; //object that is currently grabbed
	Vector3 grabPos; //position where you grab and hold objects
    private ParticleSystem particleSpray; //Need this to have something to attach to the wand
    private ParticleSystem particleStream; //Need this to have something to Destroy() later

	// Use this for initialization
	void Start () {
		//create wand
		Instantiate (wand, transform.position + new Vector3(-4.15f, 2.1f, 10.3f), Quaternion.FromToRotation(Vector3.up, transform.forward + new Vector3(0,194.3f,0)));
	} 

	// Update is called once per frame
	void Update () {
		grabPos = transform.position + (transform.forward * 3f);
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (bonkCollider, transform.position + (transform.forward * 3f), transform.rotation);
			particleSpray = (ParticleSystem) Instantiate (mouseClickParticle, (wand.transform.position + wand.transform.up), transform.rotation);
            particleSpray.gameObject.transform.parent = wand.transform;
		}
		if (Input.GetButtonDown ("Fire2")) {
			Grab ();
            particleStream = (ParticleSystem) Instantiate(grabParticle, (wand.transform.position + wand.transform.up), transform.rotation);
            particleStream.gameObject.transform.parent = wand.transform;
        }
		if (Input.GetButtonUp ("Fire2")) {
			Release ();
            Destroy(particleStream.gameObject);
		}

		//move grabbed object
		if (grabbedObject) {
			Vector3 dir = grabPos - grabbedObject.transform.position;
			dir.Normalize ();
			grabbedObject.AddForce (dir * grabforce);
			Debug.Log ("I graeb u");
		}
	}

	//searches for object to grab when RMB is clicked
	void Grab() {
		Instantiate (grabCollider, transform.position + (transform.forward * 3f), transform.rotation);
	}

	public void SetGrabbed(Rigidbody other) {
		grabbedObject = other;
	}

	void Release() {
		grabbedObject = null;
	}
}
