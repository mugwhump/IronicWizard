using UnityEngine;
using System.Collections;

public class cauldron_nom : MonoBehaviour {
    public Hashtable cauldronHas = new Hashtable(); //Let's use a Hashtable to keep track of what's in the cauldron
    public float shrinkTime = 0.5f;
    public int redCount = 0; //Some counters for recipes
    public int greenCount = 0;
    public int blueCount = 0;
    private bool isShrink = false; //For clarity, using to avoid outputting from cauldron while it is shrinking/consuming anything
    public GameObject output;

    public recipe baconRecipe = new recipe(2,1,0, bacon);
    public recipe friedEggRecipe = new recipe(2, 2, 1, friedEgg);
    public recipe espressoRecipe = new recipe(1, 2, 0, espresso);
    public recipe pancakeRecipe = new recipe(1, 2, 2, pancake);
    public recipe cerealRecipe = new recipe(0, 1, 2, cereal);
    public recipe totParpsRecipie = new recipe(1, 2, 2, totParps);
    public recipe orangeJuiceRecipie = new recipe(3, 0, 0, orangeJuice);
    public recipe teapotRecipe = new recipe(2, 2, 2, teapot);

    void OnCollisionEnter ( Collision other )
    {
        var obj = other.gameObject;
        var eatMe = obj.GetComponent<Edible>();
        if (!playerGrabbing(other.gameObject) && eatMe) {
            redCount += eatMe.redValue;
            greenCount += eatMe.greenValue;
            blueCount += eatMe.blueValue;
            StartCoroutine(shrink(shrinkTime, eatMe));
            Debug.Log(redCount + ", " + blueCount + ", " + greenCount);
        }
    }

    void Update()
    {
        if (!isShrink) //Avoiding outputting anything while the cauldron is eating things!
        {
            //Compare colourCounts to recipies
            //Subtract the requisite colourCounts
            //Spawn the object and launch it
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

    IEnumerator shrink(float time, Edible toShrink)
    {
        isShrink = true;
        Vector3 originalScale = toShrink.transform.localScale;
        Vector3 destinationScale = new Vector3(0.1f, 0.1f, 0.1f);

        float currentTime = 0.0f;

        do
        {
            toShrink.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

		toShrink.UseUp();
        isShrink = false;
    }

    public class recipe
    {
        public int redCost;
        public int greenCost;
        public int blueCost;
        public GameObject output;

        public recipe(int redC, int greenC, int blueC, GameObject outp)
        {
            redCost = redC;
            greenCost = greenC;
            blueCost = blueC;
            output = outp;
        }
    }
}