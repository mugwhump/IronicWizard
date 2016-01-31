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
            audio.mute = true;
            isOn = false;
        } else
        {
            audio.mute = false;
            isOn = true;
        }
    }
}
