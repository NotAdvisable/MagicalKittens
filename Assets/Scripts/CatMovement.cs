using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour {

    [SerializeField] private float _speed = 8;
    [SerializeField] private float _jumpStrength = 40;

    private CatController _controller;
    private Vector3 _input, _movement;
    private Rigidbody _rb;
    private Animator _anim;
    private bool _inputRun, _inputJump, _isJumping;
    private float _lookingDirection;

    void Start () {
        _controller = GetComponent<CatController>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _lookingDirection = 0;
	}
	void FixedUpdate () {
        HandleInput();
        HandleMovement();
        UpdateAnimator();
    }
    private void HandleInput() {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _inputRun = Input.GetKey(KeyCode.LeftShift);
        _inputJump = Input.GetKey(KeyCode.Space);
    }
    private void HandleMovement() {
        //adjust facing direction
        var currentSign = Mathf.Sign(_input.x);
        if (currentSign != _lookingDirection && _input.x != 0) {
            transform.forward = (_input.x >= 0) ? Vector3.right : Vector3.left;
            _lookingDirection = currentSign;
        }
        //multiply input by speed
        _movement = _input.MutliplyByValue(_inputRun ? _speed * 2 : _speed);

        //apply velocity
        _rb.velocity = new Vector3(_movement.x, _rb.velocity.y, _movement.z);


        if (_inputJump && !_isJumping) {
            _rb.AddForce(new Vector3(0, _jumpStrength, 0), ForceMode.Impulse);
            _isJumping = true;
        }
    }

    private void UpdateAnimator() {
        var value = _inputRun ? _input : _input / 2;
        _anim.SetFloat("Speed", Mathf.Max(Mathf.Abs(value.x), Mathf.Abs(value.z)));
        _anim.SetBool("Jump", _isJumping);
    }

    public void StillJumping() {
        _rb.AddForce(Vector3.down *15, ForceMode.Acceleration);
    }
    public void Land() {
        _controller.Land();
        _isJumping = false;
    }
}
