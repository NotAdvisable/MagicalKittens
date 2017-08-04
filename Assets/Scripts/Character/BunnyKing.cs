using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BunnyKing : NetworkCharacter {

    [SerializeField] GroundAttack _groundAttack;
	// Use this for initialization
	void Start () {
		
	}
	
    public void ActivateBoss()
    {

    }

    public override void Land() {
        base.Land();
        Instantiate(_groundAttack, transform.position + Vector3.up, transform.rotation);
        EventController.Singleton.ScreenShake();
    }
}
