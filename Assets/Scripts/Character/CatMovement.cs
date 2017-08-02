using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CatMovement : NetworkBehaviour {

    [SerializeField] private float _speed = 8;
    [SerializeField] private float _jumpStrength = 40;

    private CatController _controller;
    private Vector3 _input, _movement;
    private Rigidbody _rb;
    private Animator _anim;
    private bool _inputRun, _inputJump, _isJumping, _mouseClickLeft;
    private float _lookingDirection;
    private int _groundRayMask;

    void Start () {
        _controller = GetComponent<CatController>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _lookingDirection = 0;
        _groundRayMask = 1 << LayerMask.NameToLayer("Default");
    }
	void FixedUpdate () {
       if (!isLocalPlayer) return;

        HandleInput();
        HandleMovement();
        UpdateAnimator();
    }
    private void HandleInput() {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _inputRun = Input.GetKey(KeyCode.LeftShift);
        _inputJump = Input.GetKey(KeyCode.Space);
        _mouseClickLeft = Input.GetMouseButton(0);
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

        if (_isJumping) {
            Debug.DrawLine(transform.position, transform.position + (Vector3.up * 0.01f) + Vector3.down * .2f, Color.red);
            if (Physics.Raycast(transform.position + (Vector3.up * 0.01f), Vector3.down, .1f, _groundRayMask)) {
                _controller.Land();
                _isJumping = false;
                return;
            }
            _rb.AddForce(Vector3.down * 15, ForceMode.Acceleration);
        }
        if (_mouseClickLeft) {
          _controller.SpawnProjectile();
        }
    }
    private void UpdateAnimator() {
        var value = _inputRun ? _input : _input / 2;
        _anim.SetFloat("Speed", Mathf.Max(Mathf.Abs(value.x), Mathf.Abs(value.z)));
        _anim.SetBool("Jump", _isJumping);
    }
}
