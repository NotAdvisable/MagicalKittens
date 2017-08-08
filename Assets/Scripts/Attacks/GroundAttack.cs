using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttack : MonoBehaviour {

    [SerializeField] private float _damage;

	void Start () {
        Destroy(gameObject, .8f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnParticleCollision(GameObject other)
    {
       if (!other.CompareTag("Player")) return;
       var hit = other.GetComponent<IHitable>();
       if (hit != null) hit.Hit(_damage);
    }
}
