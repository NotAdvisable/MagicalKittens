using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtKingTarget : MonoBehaviour {

    [SerializeField] private BunnyKing _king;

	void FixedUpdate () {
        if (!_king.isServer) return;
        transform.LookAt(_king.GetTarget());
	}
}
