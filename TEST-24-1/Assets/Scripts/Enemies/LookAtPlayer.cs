using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class LookAtPlayer : MonoBehaviour
    {
        private Transform _playerTransform;
        [SerializeReference] private bool _lookRight;

        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
        private void Update()
        {
            if (_playerTransform != null)
            {
                if (_playerTransform.position.x - transform.position.x < 0 && _lookRight)
                {
                    Flip();
                    _lookRight = false;
                }
                else if (_playerTransform.position.x - transform.position.x > 0 && !_lookRight)
                {
                    Flip();
                    _lookRight = true;
                }
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
