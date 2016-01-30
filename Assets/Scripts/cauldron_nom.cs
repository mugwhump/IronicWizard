using UnityEngine;
using System.Collections;

public class cauldron_nom : MonoBehaviour {
    public Hashtable cauldronHas = new Hashtable(); //Let's use a Hashtable to keep track of what's in the cauldron
    public float shrinkTime = 0.5f;
    public int redCount = 0; //Some counters for recipes
    public int greenCount = 0;
    public int blueCount = 0;
    
    void OnCollisionEnter ( Collision other )
    {
        var obj = other.gameObject;
        var eatMe = obj.GetComponent<Edible>();
        if (!playerGrabbing(other.gameObject) && eatMe) {
            redCount += eatMe.redValue;
            greenCount += eatMe.greenValue;
            blueCount += eatMe.blueValue;
            StartCoroutine(shrink(shrinkTime, other.gameObject));
            Debug.Log(redCount + ", " + blueCount + ", " + greenCount);
        }
    }

    bool playerGrabbing(GameObject grabbed)
    {
        if (grabbed.GetComponent<Rigidbody>() != GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Player> ().GetGrabbed()) //Check to see if an object is currently grabbed by the player
        {
            return false;
        }
        else
        {
            return true;
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