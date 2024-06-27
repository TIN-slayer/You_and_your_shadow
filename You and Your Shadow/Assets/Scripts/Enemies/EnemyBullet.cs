using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


namespace Enemies
{
    public class EnemyBullet : MonoBehaviour
    {
        private Transform _playerTransform;
        private Rigidbody2D _rb;
        private Vector2 _direction = new();
        [SerializeField] private float _force;
        void Start()
        {
            // Подумать как передать инфу пуле
            _playerTransform = GameObject.FindWithTag("Player")?.transform;
            _rb = GetComponent<Rigidbody2D>();
            if (_playerTransform != null)
            {
                _direction = _playerTransform.position - transform.position;
                float angle = 270 - Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
                transform.Rotate(0, 0, angle);
            }
            _rb.velocity = _direction.normalized * _force;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == (int)CollisionLayers.Ground)
            {
                Destroy(gameObject);
            }
        }
    }
}
