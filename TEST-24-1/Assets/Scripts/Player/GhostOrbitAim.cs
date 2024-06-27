using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GhostOrbitAim : MonoBehaviour
    {
        [SerializeField] private Transform _ghostTransform;
        [SerializeField] private float _orbitDistansce;
        private Vector3 _oldGhostPosition;
        private void Start()
        {
            transform.position = new Vector3(_ghostTransform.position.x + _orbitDistansce, _ghostTransform.position.y, 0);
            _oldGhostPosition = _ghostTransform.position;
        }

        void Update()
        {
            if (_ghostTransform.position != _oldGhostPosition)
            {
                Vector3 direction = (_ghostTransform.position - _oldGhostPosition).normalized;
                transform.position = _ghostTransform.position - direction * _orbitDistansce;
                _oldGhostPosition = _ghostTransform.position;
            }          
        }
    }
}
