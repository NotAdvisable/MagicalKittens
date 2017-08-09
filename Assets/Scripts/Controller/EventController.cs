using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains most publicly accessible events
/// </summary>
public class EventController : MonoBehaviour {

    private static EventController _instance;

    public static EventController Singleton { get { return _instance; } }

    public event Action OnActivateBossEvent, OnLeaveBossEvent, OnScreenShakeEvent, OnEnemyDiedEvent, OnBossDiedEvent;


    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public void ActivateBoss()
    {
        if (OnActivateBossEvent != null)
        {
            OnActivateBossEvent();
        }
    }
    public void LeaveBoss()
    {
        if (OnLeaveBossEvent != null)
        {
            OnLeaveBossEvent();
        }
    }
    public void EnemyDied()
    {
        if (OnEnemyDiedEvent != null)
        {
            OnEnemyDiedEvent();
        }
    }
    public void BossDied()
    {
        if(OnBossDiedEvent != null)
        {
            OnBossDiedEvent();
        }
    }
    public void ScreenShake() {
        if(OnScreenShakeEvent != null) {
            OnScreenShakeEvent();
        }
    }
}
