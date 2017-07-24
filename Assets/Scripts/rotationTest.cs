using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z += 45;

        transform.rotation = Quaternion.Euler(rot);
    }
}
