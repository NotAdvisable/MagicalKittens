using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    private static EventController _instance;

    public static EventController Singleton { get { return _instance; } }

    public event Action OnActivateBossEvent;
    public event Action ScreenShakeEvent;
    public event Action EnemyDiedEvent;
    public event Action BossDiedEvent;

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
    public void EnemyDied()
    {
        if (EnemyDiedEvent != null)
        {
            EnemyDiedEvent();
        }
    }
    public void BossDied()
    {
        if(BossDiedEvent != null)
        {
            BossDiedEvent();
        }
    }
    public void ScreenShake() {
        if(ScreenShakeEvent != null) {
            ScreenShakeEvent();
        }
    }
}
