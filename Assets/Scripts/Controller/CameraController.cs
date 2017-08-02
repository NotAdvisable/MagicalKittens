using System.Collections;
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

        public void SetFocus(Transform t) {
            normal.Follow = shake.Follow = t;
            normal.LookAt = shake.LookAt = t;
        }
    }

    [SerializeField] private VirtualCamPair[] _cams;
    private CinemachineBrain _brain;

    private void Awake() {
        Singleton = this;
    }
    private IEnumerator Start() {

        //Wait until the lobby players have been deleted
        yield return null;

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
       var localPlayer = NetworkState.Singleton.GetCurrentPlayers().SingleOrDefault(element => element.isLocalPlayer);

        if (localPlayer == null) return;
        _cams[0].SetFocus(localPlayer.transform);
    }

    public void SetFocus(Transform t) {
        if(t == null) return;
        _cams[0].SetFocus(t);
    }
    private void OnDestroy() {
        EventController.Singleton.ScreenShakeEvent -= ShakeScreen;
    }
}
