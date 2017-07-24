using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopTowardsFinish : MonoBehaviour {

    private Transform _finish;
    private Vector3 _actualGoal;
    private float _hopSpeed;
    private Vector2 _minMaxWait = new Vector2(0f, 2.5f);

    void Start () {
        _actualGoal = _finish.position;
        _actualGoal.z = transform.position.z;

        transform.LookAt(_actualGoal);

        _hopSpeed = Random.Range(.07f, .14f);
        StartCoroutine(HopTowardsDoom());
    }
	
    IEnumerator HopTowardsDoom() {
        yield return new WaitForSeconds(Random.Range(_minMaxWait.x, _minMaxWait.y));
        GetComponent<Animator>().SetBool("Hop", true);
        while (Vector3.Distance(transform.position, _actualGoal) > .1f) {
            transform.position = Vector3.MoveTowards(transform.position, _actualGoal, _hopSpeed);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void SetFinish(Transform f) {
        _finish = f;
    }
}
