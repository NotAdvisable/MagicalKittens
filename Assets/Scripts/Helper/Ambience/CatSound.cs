using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSound : MonoBehaviour {
    [SerializeField] private AudioClip[] _catSounds = new AudioClip[4];
    private int _currentSoundID;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAudioClipID(int id)
    {
        if(id < _catSounds.Length)
        {
            _currentSoundID = id;
        }
    }
    public void PlaySound()
    {
        if (_catSounds[_currentSoundID] != null)
        {
            _audioSource.PlayOneShot(_catSounds[_currentSoundID],.5f);
        }
    }
}
