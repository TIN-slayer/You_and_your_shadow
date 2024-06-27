using General;
using Orbs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyGuard : MonoBehaviour
    {
        [SerializeField] private float _vulnarableTime;
        [SerializeField] private float _flickTime;
        private Color _guardBreakedColor = new Color32(134, 0, 255, 255);
        [SerializeField] private EnemyHealth _health;
        private SpecialStateAnimation _guardBreakedAnimation;

        private void Awake()
        {
            _health.GuardState = true;
            _guardBreakedAnimation = GetComponent<SpecialStateAnimation>();
        }

        private void OnEnable()
        {
            OrbCollection.OrbCollected += GuardBrake;
        }

        private void OnDisable()
        {
            OrbCollection.OrbCollected -= GuardBrake;
        }

        public void GuardBrake(string orbTag)
        {
            if (orbTag == "GuardBreakerOrb")
            {
                _guardBreakedAnimation.StartAnimation(_vulnarableTime, _guardBreakedColor, _flickTime);
                StartCoroutine(VulnarabilityPeriod());
            }
        }

        private IEnumerator VulnarabilityPeriod()
        {
            _health.GuardState = false;
            yield return new WaitForSeconds(_vulnarableTime);
            _health.GuardState = true;
        }
    }
}
