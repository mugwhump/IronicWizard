using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//public StartScreen screen;
	public string nextLevel;

	// Use this for initialization
	void Start () {
		StartScreen.show();
	}
	
	// Update is called once per frame
	void Update () {
		//move to next level on mouseclick
		if (Input.GetButtonDown ("Fire1")) {
			loadLevel (nextLevel);
		}
	}

	public void loadLevel(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}
}
