using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    public class PizzaTrap : MonoBehaviour
    {
        [SerializeField] private float _newSliceTime;
        [SerializeField] private float _pingTime;
        [SerializeField] private GameObject _pingPrefab;
        [SerializeField] private GameObject _spikesPrefab;
        [SerializeField] private List<Quaternion> _pizzaPieces;
        private float _time;
        private int _pizzaCount = 0;
        
        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _newSliceTime && _pizzaCount < _pizzaPieces.Count)
            {
                _time = 0;
                StartCoroutine(SpawnSpikes());
            }
        }

        private IEnumerator SpawnSpikes()
        {
            {
                GameObject ping = Instantiate(_pingPrefab, new Vector3(0, 0, 0), _pizzaPieces[_pizzaCount]);
                yield return new WaitForSeconds(_pingTime);
                Destroy(ping);
                Instantiate(_spikesPrefab, new Vector3(0, 0, 0), _pizzaPieces[_pizzaCount]);
                ++_pizzaCount;
            }
        }
    }
}
