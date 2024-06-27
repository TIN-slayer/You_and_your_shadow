using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GhostController : MonoBehaviour
    {
        private Rigidbody2D _playerRb;
        private Animator _animator;
        [SerializeField] private Transform _playerTransform;

        private void Awake()
        {
            _playerRb = _playerTransform.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            _animator.SetFloat("Speed", Mathf.Abs(_playerRb.velocity.x));
            transform.position = new Vector2(-_playerTransform.position.x, -_playerTransform.position.y);
            transform.rotation = _playerTransform.rotation;
            transform.Rotate(0, 180, 0);
        }
    }
}
