using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour {

    [SerializeField] private AudioClip _normalBGM;
    [SerializeField] private AudioClip _bossBGM;
    [SerializeField] private AudioClip _victoryBGM;
    private AudioSource _source;

	void Start () {
        _source = GetComponent<AudioSource>();
        _source.clip = _normalBGM;
        _source.Play();
        EventController.Singleton.OnLeaveBossEvent += SwitchToNormal;
        EventController.Singleton.OnActivateBossEvent += SwitchToBoss; 
        EventController.Singleton.OnBossDiedEvent += SwitchToVictory;
    }
    private void SwitchToBoss()
    {
        _source.clip = _bossBGM;
        _source.Play();
    }
    private void SwitchToVictory()
    {
        _source.clip = _victoryBGM;
        _source.Play();
    }
    private void SwitchToNormal()
    {
        _source.clip = _normalBGM;
        _source.Play();
    }
    private void OnDestroy()
    {
        EventController.Singleton.OnLeaveBossEvent -= SwitchToNormal;
        EventController.Singleton.OnActivateBossEvent -= SwitchToBoss;
        EventController.Singleton.OnBossDiedEvent -= SwitchToVictory;
    }
}
