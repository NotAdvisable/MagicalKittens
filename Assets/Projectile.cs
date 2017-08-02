using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private float _playerSpeed;

	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward *  (_playerSpeed + 20);
        Destroy(gameObject, 1f);
	}
    public void SetPlayerSpeed(float speed) {
        _playerSpeed = speed;
    }
}
