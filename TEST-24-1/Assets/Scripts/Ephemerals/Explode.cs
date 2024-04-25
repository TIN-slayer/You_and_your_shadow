using System.Collections;
using UnityEngine;
using Enemies;
using System;
using General;

namespace Ephemerals
{
    public class Explode : MonoBehaviour
    {
        [SerializeField] private float _explosionKillTime;
        [SerializeField] private float _explosionLifeTime;
        // Добавить states
        private bool _killMode = true;
        public static Action EnemyExploaded;
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
                EnemyExploaded?.Invoke();
                enemy.TakeDamage();
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
