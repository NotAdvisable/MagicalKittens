using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    private GameObject _child;
    private AudioSource _audio;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private int _secondsUntilEndScreen = 5;

    private void Start()
    {
        _child = transform.GetChild(0).gameObject;
        _audio = GetComponent<AudioSource>();
        EventController.Singleton.OnBossDiedEvent += ShowEndScreen;
    }

    private void ShowEndScreen()
    {
        StartCoroutine(WaitUntilEndScreen());
    }
    private IEnumerator WaitUntilEndScreen()
    {
        yield return new WaitForSeconds(_secondsUntilEndScreen);
        _child.SetActive(true);
    }

    public void PlayClick()
    {
        _audio.PlayOneShot(_clip);
    }
    public void StopHost()
    {
        PlayClick();
        var man = FindObjectOfType<CustomLobbyManager>();
        if (man != null) man.StopHost();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
