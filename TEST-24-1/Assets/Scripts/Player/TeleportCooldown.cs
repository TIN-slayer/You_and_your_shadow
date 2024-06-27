using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ephemerals;
using General;
using Orbs;

namespace Player
{
    public class TeleportCooldown : MonoBehaviour
    {
        // œÓ‰ÛÏ‡Ú¸
        private float _delayCooldownTime = 0.05f;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private float _flickTime;
        [SerializeField] private Color _cooldownColor;
        private bool _onCooldown = false;
        private int _cooldownCount = 0;
        public bool OnCooldown
        {
            get
            {
                return _onCooldown;
            }
        }
        private SpecialStateAnimation _cooldownAnimation;

        private void Awake()
        {
            _cooldownAnimation = GetComponent<SpecialStateAnimation>();
            _cooldownTime -= _delayCooldownTime;
            if (_cooldownTime < 0)
            {
                Debug.LogError("cooldownTime < 0");
            }
        }

        private void OnEnable()
        {
            OrbCollection.OrbCollected += StopCooldown;
            PlayerHealth.LivesChanged += StartLostLiveCooldown;
        }

        private void OnDisable()
        {
            OrbCollection.OrbCollected -= StopCooldown;
            PlayerHealth.LivesChanged -= StartLostLiveCooldown;
        }

        // ÍÓÒÚ˚Î¸ Ò ÔÓ‚ÂÍÓÈ, ˜ÚÓ Û·ËÎ ÔÓÚ‚ËÌËÍ‡ »—œ–¿¬»“‹
        public void StartCooldown()
        {
            StartCoroutine(CheckCooldown(_cooldownTime));
        }

        public void StartLostLiveCooldown(LivesChangedArgs arg)
        {
            if (arg.cooldown > 0)
            {
                StartCoroutine(CheckCooldown(arg.cooldown));
            }
        }

        private void StopCooldown(string tag)
        {
            _onCooldown = false;
        }

        private IEnumerator CheckCooldown(float cooldownTime)
        {
            ++_cooldownCount;
            _onCooldown = true;
            yield return new WaitForSeconds(_delayCooldownTime);
            if (_onCooldown)
            {
                // ¿Õ»Ã¿÷»ﬂ  ”Àƒ¿”Õ¿ («¿Ã≈Õ»“‹ Õ¿ ‘À›ÿ √Œ“Œ¬ÕŒ—“»)
                //_cooldownAnimation.StartAnimation(_cooldownTime, _cooldownColor, _flickTime);
                yield return new WaitForSeconds(cooldownTime);
                --_cooldownCount;
                if (_cooldownCount == 0)
                {
                    _onCooldown = false;
                }
            }
            else
            {
                --_cooldownCount;
            }
        }
    }
}
