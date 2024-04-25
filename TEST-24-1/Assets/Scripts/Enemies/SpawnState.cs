using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class SpawnState : MonoBehaviour
    {
        [SerializeField] private Collider2D _hitbox;
        [SerializeField] private float _invulnarableTime;
        private CooldownAnimation _cooldownAnimation;

        private void Awake()
        {
            _cooldownAnimation = GetComponent<CooldownAnimation>();
            _cooldownAnimation.CooldownTime = _invulnarableTime;
        }

        private void Start()
        {
            StartCoroutine(InvulnarabilityPeriod());
        }
        private IEnumerator InvulnarabilityPeriod()
        {
            _hitbox.enabled = false;
            _cooldownAnimation.StartAnimation();
            yield return new WaitForSeconds(_invulnarableTime);
            _hitbox.enabled = true;
        }
    }
}
