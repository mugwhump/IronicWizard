using UnityEngine;
using System.Collections;

public class Edible : MonoBehaviour {
    public int redValue = 0;
    public int blueValue = 0;
    public int greenValue = 0;
    public ingredient id = ingredient.RGB;

    Vector3 startingPos;

	public void Start() {
		startingPos = transform.position;
	}

	public void UseUp(Vector3 originalScale) {
		transform.position = startingPos;
		transform.localScale = originalScale;
	}
}
