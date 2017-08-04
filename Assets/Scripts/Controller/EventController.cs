﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    private static EventController _instance;

    public static EventController Singleton { get { return _instance; } }

    public event Action ScreenShakeEvent;

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

    public void ScreenShake() {
        if(ScreenShakeEvent != null) {
            ScreenShakeEvent();
        }
    }
}
