using UnityEngine;
using System.Collections;

public class radio : Bonkable {
    private bool isOn = false;

    //BIN BONK"D
    public override void bonked()
    {
        AudioSource audio = GetComponentInParent<AudioSource>();
        if (isOn)
        {
            audio.Stop();
            isOn = false;
        } else
        {
            audio.Play();
            isOn = true;
        }
    }
}
