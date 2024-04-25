using System;
using System.Collections;
using UImanager;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public class PlayerTeleport : MonoBehaviour
    {
        public static event Action<Vector3> Teleport;
        [SerializeField] private Transform _ghostTransform;
        private TeleportCooldown _cooldown;
        private bool _active = true;

        private void Awake()
        {
            _cooldown = GetComponent<TeleportCooldown>();
        }

        private void OnEnable()
        {
            GeneralMenus.GamePause += Disable;
            GeneralMenus.GameResume += Enable;
        }

        private void OnDisable()
        {
            GeneralMenus.GamePause -= Disable;
            GeneralMenus.GameResume -= Enable;
        }

        private void Update()
        {
            if (_cooldown.OnCooldown || !_active)
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

        private void Disable()
        {
            _active = false;
        }

        private void Enable()
        {
            _active = true;
        }
    }
}
