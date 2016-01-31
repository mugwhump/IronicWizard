using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{
	//We make a static variable to our LoadingScreen instance
	static StartScreen instance;
	//reference to gameobject with the static image
	GameObject loadingScreenImage;

	//function which executes on scene awake before the start function
	void Awake()
	{
		//find the ImageLS gameobject from the Hierarchy
		loadingScreenImage = GameObject.Find("SplashImage");
		//destroy the already existing instance, if any
		if (instance)
		{
			Destroy(gameObject);
			hide();     //call hide function to hide the 'loading Screen Sprite'
			return;
		}
		instance = this;   
		instance.loadingScreenImage.SetActive(false);
		DontDestroyOnLoad(this);  //make this object persistent between scenes
	}

	void Update()
	{
		//hide the loading screen if the scene is loaded
		if(!Application.isLoadingLevel)
			hide();
	}
	//function to enable the loading screen
	public static void show()
	{
		Debug.Log ("ANUS :D");
		//if instance does not exists return from this function
		if (!InstanceExists())
		{
			return;
		}
		//enable the loading image object
		Debug.Log ("DOUBLE ANUS :D");
		instance.loadingScreenImage.SetActive(true);
	}
	//function to hide the loading screen
	public static void hide()
	{
		if (!InstanceExists())
		{
			return;
		}
		instance.loadingScreenImage.SetActive(false);
	}
	//function to check if the persistent instance exists
	static bool InstanceExists()
	{
		if (!instance)
		{
			return false;
		}
		return true;

	}

}