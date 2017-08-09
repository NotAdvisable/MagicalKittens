using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFluffle : MonoBehaviour {

    [SerializeField] GameObject[] _bunnyPrefabs = new GameObject[4];
    [SerializeField] Vector2 _minMaxWait = new Vector2(5f, 10f);

    private Transform _start;
    private Transform _finish;

	void Start () {
        _start = transform.GetChild(0);
        _finish = transform.GetChild(1);

        StartCoroutine(SpawnBunnies());
	}
    IEnumerator SpawnBunnies() {
        while (true) {
            var randomNum = Random.Range(1, _start.childCount);
            for (int i = 0; i < randomNum; i++) {
                var go = Instantiate(_bunnyPrefabs[Random.Range(0, _bunnyPrefabs.Length - 1)], _start.GetChild(i));
                go.AddComponent<HopTowardsFinish>().SetFinish(_finish);
            }
            yield return new WaitForSeconds(Random.Range(_minMaxWait.x, _minMaxWait.y));
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
