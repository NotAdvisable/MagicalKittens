using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGround : MonoBehaviour {

    [SerializeField] private GameObject _landingSmoke;
    private float raycastLength = .3f;
    private RaycastHit hitInfo;

    //private void FixedUpdate() {
    //    checkGround();
    //}

    //private void checkGround() {
    //    if (Physics.Raycast(transform.position + (Vector3.up * 0.01f), Vector3.down, out hitInfo, raycastLength)) Land();
    //    Debug.Log(hitInfo.collider);
    //}

    public void Land() {
        if (_landingSmoke != null) Instantiate(_landingSmoke, transform.position, Quaternion.identity);
       // GetComponent<AudioSource>().PlayOneShot(_landingSound);
    }

}
