using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bossfight : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera _defaultBossCamera;
    [SerializeField] private bool _trapPlayer;
    private float _lastContactDirection;

    private void OnTriggerExit(Collider other) {
        if (!other.GetComponent<NetworkCharacter>().isLocalPlayer) return;

        var contactDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
        if (contactDirection != _lastContactDirection) {
            _defaultBossCamera.gameObject.SetActive(!_defaultBossCamera.gameObject.activeSelf);
            _lastContactDirection = contactDirection;
        }
        GetComponent<Collider>().isTrigger = !_trapPlayer;

    }

}
