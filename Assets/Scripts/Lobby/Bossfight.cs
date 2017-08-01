using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bossfight : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera _defaultBossCamera;
    private float _lastContactDirection;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerExit(Collider other) {
        var contactDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
        if (contactDirection != _lastContactDirection) {
            _defaultBossCamera.gameObject.SetActive(!_defaultBossCamera.gameObject.activeSelf);
            _lastContactDirection = contactDirection;
        }

    }

}
