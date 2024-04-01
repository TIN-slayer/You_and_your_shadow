using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBounceController : MonoBehaviour
    {
        [SerializeField] private float _initialForce;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            float alpha = Random.Range(0, 2 * Mathf.PI);
            float x = _initialForce * Mathf.Cos(alpha);
            float y = _initialForce * Mathf.Sin(alpha);
            _rb.AddForce(new Vector2(x, y));
        }
    }
}
