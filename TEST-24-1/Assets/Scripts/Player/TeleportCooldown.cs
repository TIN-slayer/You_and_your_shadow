using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ephemerals;
using General;

namespace Player
{
    public class TeleportCooldown : MonoBehaviour
    {
        // Подумать
        private float _delayCooldownTime = 0.1f;
        [SerializeField] private float _cooldownTime;
        private bool _onCooldown = false;
        public bool OnCooldown
        {
            get
            {
                return _onCooldown;
            }
        }
        private CooldownAnimation _cooldownAnimation;

        private void Awake()
        {
            _cooldownAnimation = GetComponent<CooldownAnimation>();
            _cooldownTime -= _delayCooldownTime;
            if (_cooldownTime < 0)
            {
                Debug.LogError("cooldownTime < 0");
            }
            _cooldownAnimation.CooldownTime = _cooldownTime;
        }

        private void OnEnable()
        {
            Explode.EnemyExploaded += StopCooldown;
        }

        private void OnDisable()
        {
            Explode.EnemyExploaded -= StopCooldown;
        }

        // костыль с проверкой, что убил протвиника ИСПРАВИТЬ
        public void StartCooldown()
        {
            StartCoroutine(CheckCooldown());
        }

        private void StopCooldown()
        {
            _onCooldown = false;
        }

        private IEnumerator CheckCooldown()
        {
            _onCooldown = true;
            yield return new WaitForSeconds(_delayCooldownTime);
            if (_onCooldown)
            {
                _cooldownAnimation.StartAnimation();
                yield return new WaitForSeconds(_cooldownTime);
                _onCooldown = false;
            }
        }
    }
}
