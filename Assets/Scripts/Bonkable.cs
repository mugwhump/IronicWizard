using UnityEngine;
using System.Collections;

public class Bonkable : MonoBehaviour
{
    public virtual void bonked() { Debug.Log("Sadness!"); }
}