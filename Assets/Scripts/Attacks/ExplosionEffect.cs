using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    [SerializeField] bool _triggerScreenShake;

	void Start () {
        if (_triggerScreenShake && EventController.Singleton != null) EventController.Singleton.ScreenShake();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
