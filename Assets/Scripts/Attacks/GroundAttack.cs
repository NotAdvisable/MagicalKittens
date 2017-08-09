using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttack : MonoBehaviour {

    [SerializeField] private float _damage = 200f;

	void Start () {
        Destroy(gameObject, .8f);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (!other.CompareTag("Player")) return;
       var hit = other.GetComponent<IHitable>();
       if (hit != null) hit.Hit(_damage,null);
    }
}
