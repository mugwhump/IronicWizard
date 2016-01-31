using UnityEngine;
using System.Collections;

public class cauldron_nom : Bonkable
{
    public Hashtable cauldronHas = new Hashtable(); //Let's use a Hashtable to keep track of what's in the cauldron
    public float foodForce = 50.0f;
    public float shrinkTime = 0.5f;
    public int redCount = 0; //Some counters for recipes
    public int greenCount = 0;
    public int blueCount = 0;
    private bool isShrink = false; //For clarity, using to avoid outputting from cauldron while it is shrinking/consuming anything
    ingredient cookwareValue = ingredient.RGB;

    void OnCollisionEnter(Collision other)
    {
        var obj = other.gameObject;
        var eatMe = obj.GetComponent<Edible>();
        if (!playerGrabbing(other.gameObject) && eatMe && !isShrink)
        {
            redCount += eatMe.redValue;     //Consume the RGB values
            greenCount += eatMe.greenValue;
            blueCount += eatMe.blueValue;
            if (eatMe.id != ingredient.RGB) //Keep the value of the last cookware item fed to the cauldron
            {
                cookwareValue = eatMe.id;
            }          
            StartCoroutine(shrink(shrinkTime, eatMe));
        }
    }

    void altBonk()
    {
       if (cookwareValue != ingredient.RGB) //If we've actually got cookware in the cauldron
        {
            recipeCheck(cookwareValue);
            cookwareValue = ingredient.RGB;
        }
    }

    bool playerGrabbing(GameObject grabbed)
    {
        if (grabbed.GetComponent<Rigidbody>() != GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().GetGrabbed()) //Check to see if an object is currently grabbed by the player
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
        Vector3 destinationScale = new Vector3(0.1f,0.1f,0.1f);

        float currentTime = 0.0f;

        do
        {
            toShrink.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        toShrink.UseUp(originalScale);
        isShrink = false;
    }

    void recipeCheck(ingredient cookware)
    {
        switch (cookware)
        {
            case ingredient.fryPan:
                if (redCount >= 2 && greenCount >= 1)
                {
                    redCount -= 2;
                    greenCount -= 1;
                    //spawnFood(friedEgg);
                    //apply force to launch friedEgg
                }
                break;
            case ingredient.saucePan:
                if (redCount >= 1 && greenCount >= 2)
                {
                    redCount -= 1;
                    greenCount -= 2;
                    //spawnFood(hardBoiledEgg);
                    //apply force to launch hardBoiledEgg
                }
                break;
            case ingredient.coffeeThing:
                if (redCount >= 2 && blueCount >= 1)
                {
                    redCount -= 2;
                    blueCount -= 1;
                    //spawnFood(coffee);
                    //apply force to launch coffee
                }
                break;
            case ingredient.waffleIron:
                if (redCount >= 1 && blueCount >= 2)
                {
                    redCount -= 1;
                    blueCount -= 2;
                    //spawnFood(waffles);
                    //apply force to launch waffles
                }
                break;
            case ingredient.juicer:
                if (greenCount >= 2 && blueCount >= 1)
                {
                    greenCount -= 2;
                    blueCount -= 1;
                    //spawnFood(orangeJuice);
                    //apply force to launch orangeJuice
                }
                break;
            case ingredient.toaster:
                if (greenCount >= 1 && blueCount >= 2)
                {
                    greenCount -= 1;
                    blueCount -= 2;
                    //spawnFood(toast);
                    //apply force to launch toast
                }
                break;
            default:
                Debug.Log("uhhh");
                break;
        }
    }

    void spawnFood(GameObject food)
    {
        //GameObject food = Instantiate(food, (transform.position + transform.up * 2), transform.rotation)
        //food.Rigidbody.AddForce(-1 * transform.position * foodForce, ForceMode.Impulse)
    }
}