using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocoState : StateMachineBehaviour {

    [SerializeField] private float _minSecondsBetweenIdles = 3;
    [SerializeField] private float _maxSecondsBetweenIdles = 6;
    private float _secondsBetweenIdles;
    private float _currentWait, _velocity;
    private float _newIdle, _currentIdle;
    private int _randomIdle;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        _randomIdle = Animator.StringToHash("RandomIdle");
        NewIdleWait();

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _currentWait += Time.deltaTime;

        if (_secondsBetweenIdles < _currentWait) {
            _newIdle = Random.Range(-1, 2);
            NewIdleWait();
            _currentWait = 0;
        }

        if (Mathf.Abs(_newIdle - _currentIdle) < 0.001f) {
            _newIdle = 0;
        }

        _currentIdle = Mathf.Clamp(Mathf.SmoothDamp(_currentIdle, _newIdle, ref _velocity, .5f), -1f, 1f);
        animator.SetFloat(_randomIdle, _currentIdle);
    }

    private void NewIdleWait() {
        _secondsBetweenIdles = Random.Range(_minSecondsBetweenIdles, _maxSecondsBetweenIdles + 1f);
    }
}
