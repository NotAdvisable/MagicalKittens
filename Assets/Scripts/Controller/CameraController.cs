using System.Collections;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public static CameraController Singleton { get; private set; }
    /// <summary>
    /// Groups the normal cam and shake cam together as a pair
    /// </summary>
    [Serializable]
    private struct VirtualCamPair
    {
        public CinemachineVirtualCamera normal;
        public CinemachineVirtualCamera shake;

        public void SetFocus(Transform t)
        {
            normal.Follow = shake.Follow = t;
            normal.LookAt = shake.LookAt = t;
        }
    }

    [SerializeField]
    private VirtualCamPair[] _cams;
    private CinemachineBrain _brain;

    private void Awake()
    {
        Singleton = this;
    }
    private IEnumerator Start()
    {

        //Wait until the lobby players have been deleted
        yield return null;

        _brain = Camera.main.GetComponent<CinemachineBrain>();
        EventController.Singleton.OnScreenShakeEvent += ShakeScreen;

        StartCoroutine(FindLocalPlayer());
    }
    /// <summary>
    /// Toggles the Shake when the event is triggered
    /// </summary>
    private void ShakeScreen()
    {
        var camPair = _cams.FirstOrDefault(pair => pair.normal == (UnityEngine.Object)_brain.ActiveVirtualCamera);
        if(!camPair.Equals(new VirtualCamPair()))
        {
            StartCoroutine(CamShake(camPair));
        }

    }
    private IEnumerator CamShake(VirtualCamPair pair)
    {
        pair.shake.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);
        pair.shake.gameObject.SetActive(false);
    }

    /// <summary>
    /// Finds the local player to focus on and follow at the start of the game
    /// </summary>
    /// <returns></returns>
    private IEnumerator FindLocalPlayer()
    {
        WaitForSeconds waitObject = new WaitForSeconds(0.3f);
        while (true)
        {
            var localPlayer = NetworkState.Singleton.GetCurrentPlayers().SingleOrDefault(element => element.isLocalPlayer);

            if (localPlayer)
            {
                _cams[0].SetFocus(localPlayer.transform);
            }
            yield return waitObject;
        }
    }
    public void TurnOffBossCam()
    {
        _cams[1].normal.gameObject.SetActive(false);
    }
    public void TurnOnBossCam()
    {
        _cams[1].normal.gameObject.SetActive(true);
    }
    public void SetFocus(Transform t)
    {
        if (t == null) return;
        _cams[0].SetFocus(t);
    }
    private void OnDestroy()
    {
        EventController.Singleton.OnScreenShakeEvent -= ShakeScreen;
    }
}
