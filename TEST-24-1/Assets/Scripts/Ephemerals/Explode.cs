using System.Collections;
using UnityEngine;
using Enemies;
using System;
using General;
using Orbs;

namespace Ephemerals
{
    public class Explode : MonoBehaviour
    {
        [SerializeField] private float _explosionKillTime;
        [SerializeField] private float _explosionLifeTime;
        // Добавить states
        private bool _killMode = true;
        public static Action SuccessExploasion;
        private void Start()
        {
            // Круто, можно использовать везде, но мне лень :(
            _explosionLifeTime -= _explosionKillTime;
            if (_explosionLifeTime <= 0)
            {
                Debug.LogError("explosionTime error");
            }
            StartCoroutine(ExplosionLifePeriod());
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            // Подумать насчет изТриггера
            if (enemy != null && _killMode)
            {
                SuccessExploasion?.Invoke();
                enemy.TakeDamage();
                return;
            }

            OrbCollection orb = collision.gameObject.GetComponent<OrbCollection>();
            // Подумать насчет изТриггера
            if (orb != null && _killMode)
            {
                SuccessExploasion?.Invoke();
                orb.GetCollected();
            }
        }

        private IEnumerator ExplosionLifePeriod()
        {
            yield return new WaitForSeconds(_explosionKillTime);
            _killMode = false;
            yield return new WaitForSeconds(_explosionLifeTime);
            Destroy(gameObject);
        }
    }
}
