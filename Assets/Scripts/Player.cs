using UnityEngine;
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
    private ParticleSystem particleSpray; //Need this to have something to attach to the wand
    private ParticleSystem particleStream; //Need this to have something to Destroy() later

	// Use this for initialization
	void Start () {
		//create wand
		Instantiate (wand, transform.position + new Vector3(-4.15f, 2.1f, 10.3f), Quaternion.FromToRotation(Vector3.up, transform.forward + new Vector3(0,194.3f,0)));
		colliderInstance = Instantiate (grabCollider, transform.position + (transform.forward * 3f), transform.rotation) as Collider;
	} 

	// Update is called once per frame
	void Update () {
		colliderInstance.transform.position = transform.position + (transform.forward * grabDist);
		if (Input.GetButtonDown ("Fire1")) {
			Bonk ();
		}
		if (Input.GetButtonDown ("Fire2")) {
			Grab ();
        }
		if (Input.GetButtonUp ("Fire2")) {
			Release ();
		}

		//move grabbed object
		if (activeObj && grabbing) {
			Vector3 dir = colliderInstance.transform.position - activeObj.transform.position;
			dir.Normalize ();
			activeObj.AddForce (dir * grabforce);
		}
	}

	//Bonks active object
	void Bonk() {
        particleSpray = (ParticleSystem)Instantiate(mouseClickParticle, (wand.transform.position + wand.transform.up), transform.rotation);
        particleSpray.gameObject.transform.parent = wand.transform;

        if (activeObj == null)
        {
            return;
        }
        else if (activeObj.gameObject.GetComponent<Bonkable>())
        {
            Bonkable bonk_me = activeObj.gameObject.GetComponent<Bonkable>();
            bonk_me.bonked();
            activeObj.AddForce(transform.forward * bonkForce / 5, ForceMode.Impulse);
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
		particleStream = (ParticleSystem) Instantiate(grabParticle, (wand.transform.position + wand.transform.up), transform.rotation);
		particleStream.gameObject.transform.parent = wand.transform;
	}

	void Release() {
		grabbing = false;
        if(GameObject.FindObjectOfType<ParticleSystem>())
        {
            Destroy(particleStream.gameObject);
        }       
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