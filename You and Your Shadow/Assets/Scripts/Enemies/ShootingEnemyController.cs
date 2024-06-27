using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class ShootingEnemyController : MonoBehaviour
    {
        [SerializeField] private float _shootingCooldown;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Collider2D _hitbox;
        public static Action StartedShoot;
        private float _timer;
        private bool _startedShoot = false;

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _shootingCooldown && _hitbox.enabled)
            {
                _timer = 0f;
                Shoot();
            }
        }

        private void Shoot()
        {
            if (!_startedShoot)
            {
                _startedShoot = true;
                StartedShoot?.Invoke();
            }
            Instantiate(_bullet, transform.position, new Quaternion());
        }
    }
}
