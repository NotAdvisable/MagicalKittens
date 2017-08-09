using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bossfight : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _defaultBossCamera;
    [SerializeField] private bool _trapPlayer;
    private float _lastContactDirection;

    //Activates and deactivates the boss cam, calls boss events etc. depending on from what side you enter the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CatController>() == null) return;

        var contactDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
        if (contactDirection < 0)
        {
            EventController.Singleton.ActivateBoss();
        }
        else
        {
            EventController.Singleton.LeaveBoss();
        }

        if (contactDirection != _lastContactDirection)
        {
            if (!other.GetComponent<NetworkCharacter>().isLocalPlayer) return;
            _defaultBossCamera.gameObject.SetActive(!_defaultBossCamera.gameObject.activeSelf);
            _lastContactDirection = contactDirection;
        }
        GetComponent<Collider>().isTrigger = !_trapPlayer;

    }
    public void ResetDirection()
    {
        _lastContactDirection = 0;
    }

}
