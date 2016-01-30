using UnityEngine;
using System.Collections;

public class cauldron_nom : MonoBehaviour {
    public Hashtable cauldronHas = new Hashtable(); //Let's use a Hashtable to keep track of what's in the cauldron
    public float shrinkTime = 0.5f;
    private int increment = 1; //We add one thing at a time
    private int prev = 0; //We start with nothing in the cauldron
    private string key = null; //Names are annoying

	// Use this for initialization
	void Start () {
        
	}

	// Update is called once per frame
	void Update () {
        	
	}

    void OnTriggerEnter ( Collider other )
    {
        if (other.gameObject.CompareTag("Edible")) { 
            key = other.name;
            if (!cauldronHas.ContainsKey(key))
            {
                cauldronHas[key] = increment;
                StartCoroutine(shrink(shrinkTime, other.gameObject));
                Debug.Log("Nom: " + key);
            }
            else
            {
                int prev = (int)cauldronHas[key];
                cauldronHas[key] = prev + increment;
                StartCoroutine(shrink(shrinkTime, other.gameObject));
                Debug.Log("Nom: " + key + " " + cauldronHas[key]);
            }
        }
    }

    bool playerGrabbing(GameObject obj)
    {
        //if (obj.GetComponent<Rigidbody>() == GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Player> ().SetGrabbed (rb)) //Check to see if an object is currently grabbed by the player
        {
            return false;
        }
    }

    IEnumerator shrink(float time, GameObject toShrink)
        {
            Vector3 originalScale = toShrink.transform.localScale;
            Vector3 destinationScale = new Vector3(0.1f, 0.1f, 0.1f);

            float currentTime = 0.0f;

            do
            {
                toShrink.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime <= time);

            Destroy(toShrink);
        }
}