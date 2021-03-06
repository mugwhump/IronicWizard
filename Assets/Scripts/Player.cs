﻿using UnityEngine;
using System.Collections;

//This class has stuff the player does :DDDD
public class Player : MonoBehaviour {
	//public Collider bonkCollider; //hitbox for bonking stuff
	public Collider grabCollider; //hitbox for grabbing stuff
    public ParticleSystem mouseClickParticle; //the mousey-clicky particle effect
    public ParticleSystem grabParticle; //the grabby particle effect
    public Collider wand; //Where the magic comes from!
	public Material outlineMat; //Shader used to hilight active object

    public float grabforce; //force applied every frame to grabbed object
	public float bonkForce;
    public float grabDist = 6; //distance to keep the grabbed object

	Rigidbody activeObj = null; //object that is currently grabbed
	Material[] oldMats = null; //original material array of active object
	Collider colliderInstance = null;
	bool grabbing = false; //whether activeObj is being grabbed

	AudioSource source;

	// Use this for initialization
	void Start () {
		source = gameObject.AddComponent<AudioSource> ();
//		if (source == null)
		//create wand
		Instantiate (wand, transform.position + new Vector3(-4.15f, 2.1f, 10.3f), Quaternion.FromToRotation(Vector3.up, transform.forward + new Vector3(0,194.3f,0)));
		colliderInstance = Instantiate (grabCollider, transform.position + (transform.forward * 3f), transform.rotation) as Collider;
	} 

	// Update is called once per frame
	void Update () {
		colliderInstance.transform.position = transform.position + (transform.forward * grabDist);
		if (Input.GetButtonDown ("Fire1")) {
			Bonk ();
            //play the click partcle effect
            mouseClickParticle.Play();
			if (Random.value < .07f) {
				Debug.Log ("audio");
				AudioClip clip = GameObject.FindGameObjectWithTag ("SoundController").GetComponent<SoundController> ().GetSound (soundType.wizardSpells);
				source.clip = clip;
				source.Play ();
			}
        }
		if (Input.GetButtonDown ("Fire2")) {
			Grab ();
			grabParticle.Play();
			if (Random.value < .07f) {
				AudioClip clip = GameObject.FindGameObjectWithTag ("SoundController").GetComponent<SoundController> ().GetSound (soundType.wizardSpells);
				source.clip = clip;
				source.Play ();
			}
        }
		if (Input.GetButtonUp ("Fire2")) {
			Release ();
            grabParticle.Stop();
		}

		//move grabbed object
		if (activeObj && grabbing) {
			Vector3 dir = colliderInstance.transform.position - activeObj.transform.position;
			dir.Normalize ();
			float dist = Vector3.Distance (colliderInstance.transform.position, activeObj.transform.position);
			float speed = activeObj.GetPointVelocity (activeObj.transform.position).magnitude;

			activeObj.AddForce (dir * grabforce * dist * (dist < speed * 2 ? .2f : 1));
//			activeObj.AddForce (dir * grabforce * (dist < .25f ? dist : 1));
		}
	}

	//Bonks active object
	void Bonk() {       
        if (activeObj == null)
        {
            return;
        }
        else if (activeObj.gameObject.GetComponent<Bonkable>())
        {
            Bonkable bonk_me = activeObj.gameObject.GetComponent<Bonkable>();
            bonk_me.bonked();
            activeObj.AddForce(transform.forward * bonkForce, ForceMode.Impulse);
        }
        else
        {
            activeObj.AddForce(transform.forward * bonkForce, ForceMode.Impulse);
        }
	}

	//Grabs active object
	void Grab() {
		if (activeObj == null)
			return;
		grabbing = true;
		activeObj.useGravity = false;
	}

	void Release() {
		grabbing = false;
		activeObj.useGravity = true;      
	}

	//called when object other enters your collider
	public void SetActive(Rigidbody other) {
		if (activeObj == other || grabbing) //don't reactivate same object. Grabbing locks active object.
			return;
		if (activeObj != null) {
			SetInactive (activeObj);
		}
		activeObj = other;
		MeshRenderer mr = other.GetComponent<MeshRenderer>();
		if ( mr ) {
			oldMats = mr.materials;
			Material[] mats = new Material[mr.materials.Length];
			for (int i = 0; i < mats.Length; i++) {
				mats [i] = outlineMat;
				mats [i].mainTexture = mr.materials [i].mainTexture;
			}
			mr.materials = mats;
		}
	}

	//Called when object leaves collider
	public void SetInactive(Rigidbody other) {
		if (activeObj == other && !grabbing) { //don't let go if you turn quickly while grabbing
			activeObj = null;
			MeshRenderer mr = other.GetComponent<MeshRenderer> ();
			if (mr) {
				mr.materials = oldMats;
				oldMats = null; 
			}
		}
	}

	public Rigidbody GetGrabbed() {
		if (activeObj != null && grabbing) {
			return activeObj;
		} else {
			return null;
		}
	}
}