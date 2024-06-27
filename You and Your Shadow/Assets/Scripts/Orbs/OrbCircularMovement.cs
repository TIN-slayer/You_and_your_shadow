using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbs
{
    public class OrbCircularMovement : MonoBehaviour
    {
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _radius;
        private float _angle = 0f;

        private void Start()
        {
            if (Random.Range(0, 2) == 0)
            {
                _angularSpeed *= -1;
            }
            _angle = Random.Range(0, 360);
            UpdatePosition();
        }

        private void FixedUpdate()
        {
            _angle = (_angle + _angularSpeed) % 360;
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            float x = _radius * Mathf.Cos(_angle);
            float y = _radius * Mathf.Sin(_angle);
            transform.position = new Vector3(x, y, 0);
        }
    }
}
