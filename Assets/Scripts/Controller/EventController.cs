using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    public static EventController Singleton { get; private set; }

    public event Action ScreenShakeEvent;

    private void Awake() {
        Singleton = this;
    }

    public void ScreenShake() {
        if(ScreenShakeEvent != null) {
            ScreenShakeEvent();
        }
    }
}
