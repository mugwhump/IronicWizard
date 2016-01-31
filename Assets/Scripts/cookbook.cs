using UnityEngine;

public class cookbook : MonoBehaviour {
    public Texture winGame;

    public void ChangeTexture()
    {
        GetComponentInParent<Renderer>().material.mainTexture = winGame;
    }
}
