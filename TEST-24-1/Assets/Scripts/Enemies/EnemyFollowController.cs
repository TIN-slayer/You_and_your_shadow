using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyFollowController : MonoBehaviour
    {
        private Transform _playerTransform;
        [SerializeField] private float _moveSpeed;

        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            if (_playerTransform != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _moveSpeed * Time.deltaTime);
            }
        }
    }
}
