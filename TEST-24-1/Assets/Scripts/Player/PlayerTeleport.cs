using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public class PlayerTeleport : MonoBehaviour
    {
        public static event Action<Vector3> Teleport;
        [SerializeField] private Transform _ghostTransform;
        private TeleportCooldown _cooldown;

        private void Awake()
        {
            _cooldown = GetComponent<TeleportCooldown>();
        }

        private void Update()
        {
            if (_cooldown.OnCooldown)
            {
                return;
            }
            if (Input.GetButtonDown("Jump"))
            {
                Teleport?.Invoke(_ghostTransform.position);
                (transform.position, _ghostTransform.position) = (_ghostTransform.position, transform.position);
                _cooldown.StartCooldown();
            }
        }    
    }
}
