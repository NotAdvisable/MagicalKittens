using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayernameDisplay : MonoBehaviour {
    private Renderer _renderer;

	void Start () {
        _renderer = GetComponent<Renderer>();
        _renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Tab)){
            _renderer.enabled = true;
            transform.LookAt(2 * transform.position - Camera.main.transform.position);
        }
        if (Input.GetKeyUp(KeyCode.Tab)) {
            _renderer.enabled = false;
        }
	}
}
