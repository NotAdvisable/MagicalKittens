using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

public class CameraController : MonoBehaviour {

    [Serializable]
    private struct VirtualCamPair{
        public CinemachineVirtualCamera normal;
        public CinemachineVirtualCamera shake;
    }

    [SerializeField] private VirtualCamPair[] _cams;
    private CinemachineBrain _brain;

	void Start () {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        EventController.Singleton.ScreenShakeEvent += ShakeScreen;
	}
    private void ShakeScreen() {
        StartCoroutine(CamShake(_cams.Single(pair => pair.normal == (UnityEngine.Object)_brain.ActiveVirtualCamera)));
    }
    private IEnumerator CamShake(VirtualCamPair pair) {
        pair.shake.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);
        pair.shake.gameObject.SetActive(false);
    }
    private void OnDestroy() {
        EventController.Singleton.ScreenShakeEvent -= ShakeScreen;
    }
}
