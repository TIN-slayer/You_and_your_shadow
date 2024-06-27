using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Animator _animator;
        [SerializeField] private float _moveSpeed;
        private Vector2 _playerInput;
        private bool _lookRight = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
        private void FixedUpdate()
        {
            _rb.velocity = _playerInput * _moveSpeed;
            _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
            if (_rb.velocity.x < -0.1 && _lookRight)
            {
                Flip();
                _lookRight = false;
            }
            else if (_rb.velocity.x > 0.1 && !_lookRight)
            {
                Flip();
                _lookRight = true;
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
