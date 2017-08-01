using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

public class CameraController : MonoBehaviour {
    public static CameraController Singleton { get; private set; }
    [Serializable]
    private struct VirtualCamPair{
        public CinemachineVirtualCamera normal;
        public CinemachineVirtualCamera shake;
    }

    [SerializeField] private VirtualCamPair[] _cams;
    private CinemachineBrain _brain;

    private void Awake() {
        Singleton = this;
    }
    private void Start () {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        EventController.Singleton.ScreenShakeEvent += ShakeScreen;

        FindLocalPlayer();
	}
    private void ShakeScreen() {
        StartCoroutine(CamShake(_cams.Single(pair => pair.normal == (UnityEngine.Object)_brain.ActiveVirtualCamera)));
    }
    private IEnumerator CamShake(VirtualCamPair pair) {
        pair.shake.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);
        pair.shake.gameObject.SetActive(false);
    }

    private void FindLocalPlayer() {
       var localPlayer = NetworkState.Singleton.GetCurrentPlayers().Single(current => current.isLocalPlayer);

        if (localPlayer == null) return;

        _brain.ActiveVirtualCamera.LookAt = localPlayer.transform;
        _brain.ActiveVirtualCamera.Follow = localPlayer.transform;
    }

    public void SetFocus(Transform t) {
        _brain.ActiveVirtualCamera.LookAt = t;
        _brain.ActiveVirtualCamera.Follow = t;
    }
    private void OnDestroy() {
        EventController.Singleton.ScreenShakeEvent -= ShakeScreen;
    }
}
