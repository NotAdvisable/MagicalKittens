using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCat : MonoBehaviour {
    bool _hitCat; //Don't hit cats
    Collider _col;

    private void Start() {
        _col = GetComponent<Collider>();
    }
    void FixedUpdate () {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo);
            _hitCat = (hitInfo.collider == _col);
        }

        if (_hitCat && Input.GetMouseButton(0)) {
            transform.Rotate(-Vector3.up * Input.GetAxis("Mouse X"));
        }
        if (Input.GetMouseButtonUp(0)) {
            _hitCat = false;
        }
    }
}
