﻿using UnityEngine;
using System.Collections;

public class cauldron_nom : Bonkable
{
    public Hashtable cauldronHas = new Hashtable(); //Let's use a Hashtable to keep track of what's in the cauldron
    public float foodForce = 50.0f;
    public float shrinkTime = 0.5f;
    public int redCount = 0; //Some counters for recipes
    public int greenCount = 0;
    public int blueCount = 0;
    public GameObject friedEgg;
    public GameObject toast;
    public GameObject orangeJuice;
    public Transform eggyPlane;
    public Transform juicePlane;
    public Transform toastPlane;
    public int score = 3;
    public cookbook winBook;
    private bool cookEggs = false;
    private bool cookJuice = false;
    private bool cookToast = false;
    private Vector3 eggWin;
    private Vector3 toastWin;
    private Vector3 juiceWin;
    private bool isShrink = false; //For clarity, using to avoid outputting from cauldron while it is shrinking/consuming anything
    public ingredient cookwareValue = ingredient.RGB;
	public AudioClip sploosh;

    //void OnTriggerEnter(Collider other)
    void OnTriggerStay(Collider other)
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
			GetComponent<AudioSource> ().PlayOneShot (sploosh);
        }
    }

    void Start()
    {
        eggWin = eggyPlane.position;
        Vector3 eggStart = new Vector3(eggWin.x, -1.0f, eggWin.z);
        eggyPlane.position = eggStart;
        Debug.Log(eggStart + ", " + eggyPlane.position);

        toastWin = toastPlane.position;
        Vector3 toastStart = new Vector3(toastWin.x, -1.0f, toastWin.z);
        toastPlane.position = toastStart;

        juiceWin = juicePlane.position;
        Vector3 juiceStart = new Vector3(juiceWin.x, -1.0f, juiceWin.z);
        juicePlane.position = juiceStart;
    }

    void Update()
    {
        if(score == 3)
        {
            winBook.ChangeTexture(); // YOU'RE WINNER!
        }
    }

    public override void bonked()
    {
       if(redCount+blueCount+greenCount <= -15)
        {
            Debug.Log("TEAPOTTTTTTTTTTTT");
        } 
       else if (cookwareValue != ingredient.RGB) //If we've actually got cookware in the cauldron
        {
            Debug.Log(this.gameObject.name + "bonked!");
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
        isShrink = false;
        toShrink.UseUp(originalScale);
    }

    void recipeCheck(ingredient cookware)
    {
        AudioSource audio = GetComponentInParent<AudioSource>();
        audio.Play();
        
        switch (cookware)
        { 
            case ingredient.fryPan:
                if (redCount >= 2 && greenCount >= 1)
                {
                    redCount -= 2;
                    greenCount -= 1;
                    Instantiate(friedEgg, (this.transform.position + this.transform.forward * 5), this.transform.rotation);
                    if (!cookEggs)
                    {
                        score += 1;
                        cookEggs = true;
                        eggyPlane.transform.position = eggWin;
                    }
                }
                break;
            case ingredient.juicer:
                if (greenCount >= 1 && blueCount >= 2)
                {
                    greenCount -= 1;
                    blueCount -= 2;
                    Instantiate(orangeJuice, (this.transform.position + this.transform.forward * 5), this.transform.rotation);
                    if (!cookJuice)
                    {
                        score += 1;
                        cookJuice = true;
                        juicePlane.transform.position = juiceWin;
                    }
                }
                break;
            case ingredient.toaster:
                if (redCount >=1 && greenCount >= 1 && blueCount >= 1)
                {
                    redCount -= 1;
                    greenCount -= 1;
                    blueCount -= 2;
                    Instantiate(toast, (this.transform.position + this.transform.forward * 5), this.transform.rotation);
                    if (!cookToast)
                    {
                        score += 1;
                        cookToast = true;
                        toastPlane.transform.position = toastWin;
                    }
                }
                break;
            default:
                Debug.Log("uhhh");
                break;
        }
    }
}