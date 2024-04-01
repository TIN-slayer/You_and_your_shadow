using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class SneakyTrap : MonoBehaviour
    {
        [SerializeField] private float _minTime;
        [SerializeField] private float _maxTime;
        [SerializeField] private float _pingTime;
        [SerializeField] private float _sawTime;
        [SerializeField] private GameObject _pingPrefab;
        [SerializeField] private GameObject _sawPrefab;
        private Transform _playerTransform;


        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player")?.transform;
            StartCoroutine(LifeCycle());
        }

        private IEnumerator LifeCycle()
        {
            yield return new WaitForSeconds(Random.Range(_minTime, _maxTime));
            if (_playerTransform != null)
            {
                Vector3 pos = _playerTransform.position;
                GameObject ping = Instantiate(_pingPrefab, pos, new Quaternion());
                yield return new WaitForSeconds(_pingTime);
                Destroy(ping);
                GameObject saw = Instantiate(_sawPrefab, pos, new Quaternion());
                saw.GetComponent<SpriteRenderer>().sortingOrder = 0;
                yield return new WaitForSeconds(_sawTime);
                Destroy(saw);
                StartCoroutine(LifeCycle());
            }

        }
    }
}
