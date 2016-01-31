using UnityEngine;
using System.Collections;

public class cookbook_Bonk : Bonkable {

    public bool isOpen = false;
    private int page = 0;

    public override void bonked()
    {
        if (isOpen)
        {
            //increment through the spritesheet
            page += 1;
        }
        else
        {
            //open the cookbook to page 0
            isOpen = true;
        }
    }
}
