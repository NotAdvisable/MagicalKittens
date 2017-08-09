using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCat : MonoBehaviour {
    bool _hitCat; //Don't hit cats
    Collider _col;
    CatSound _sound;
    SkinnedMeshRenderer _renderer;

    private void Start() {
        _col = GetComponent<Collider>();
        _sound = FindObjectOfType<CatSound>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    void Update () {
        if (_renderer.materials.Length == 0) return;

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo);
            _hitCat = (hitInfo.collider == _col);
            if(_hitCat) _sound.PlaySound();
        }

        if (_hitCat && Input.GetMouseButton(0)) {
            transform.Rotate(-Vector3.up * 10 * Input.GetAxis("Mouse X"));
        }
        if (Input.GetMouseButtonUp(0)) {
            _hitCat = false;
        }
    }
}
