using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour {

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _UIButtonSound;
    [SerializeField] private AudioClip _UIToggleSound;

    public void PlayButtonSound()
    {
        _audioSource.PlayOneShot(_UIButtonSound);
    }
    public void PlayToggleSound(bool valid)
    {
      if(valid)  _audioSource.PlayOneShot(_UIToggleSound);
    }
}
