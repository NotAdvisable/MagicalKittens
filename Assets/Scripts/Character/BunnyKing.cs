using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BunnyKing : NetworkCharacter {

	// Use this for initialization
	void Start () {
		
	}
	
    public void ActivateBoss()
    {

    }

    public override void Land() {
        base.Land();
        EventController.Singleton.ScreenShake();
    }
}
