using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour {

    private GameObject _child;
    private AudioSource _audio;
    [SerializeField] private AudioClip _clip;

    private void Start () {
        _child = transform.GetChild(0).gameObject;
        _audio = GetComponent<AudioSource>();
	}

	private void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _child.SetActive(!_child.activeSelf);
            PlayClick();
        }
	}
    public void PlayClick()
    {
        _audio.PlayOneShot(_clip);
    }
    public void Continue()
    {
        PlayClick();
        _child.SetActive(false);
    }
    public void StopHost()
    {
        PlayClick();
        var man = FindObjectOfType<CustomLobbyManager>();
        if (man != null) man.StopHost();
    }
}
