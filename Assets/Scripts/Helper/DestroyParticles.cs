using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour {

    private ParticleSystem _ps;
    [SerializeField] private float _fallbackDuration = 3f;

	void Start () {
        _ps = GetComponentInChildren<ParticleSystem>();
        float duration;
        if (_ps == null) duration = _fallbackDuration;
        else duration = _ps.main.duration;

        Destroy(gameObject, duration);
	}
}
