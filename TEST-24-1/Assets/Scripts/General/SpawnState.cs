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
        [SerializeField] private Color _invulnarableColor;
        private SpecialStateAnimation _spawnAnimation;

        private void Awake()
        {
            _spawnAnimation = GetComponent<SpecialStateAnimation>();
        }

        private void Start()
        {
            _spawnAnimation.StartAnimation(_invulnarableTime, _invulnarableColor);
            StartCoroutine(InvulnarabilityPeriod());
        }
        private IEnumerator InvulnarabilityPeriod()
        {
            _hitbox.enabled = false;
            yield return new WaitForSeconds(_invulnarableTime);
            _hitbox.enabled = true;
        }
    }
}
