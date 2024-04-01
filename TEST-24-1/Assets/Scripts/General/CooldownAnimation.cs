using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class CooldownAnimation: MonoBehaviour
    {
        [SerializeField] private float _flickTime;
        private SpriteRenderer _spriteRenderer;
        private float _cooldownTime;

        /*private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }*/

        public float CooldownTime
        {
            set
            {
                _cooldownTime = value;
            }
        }
        private Color _fullColor;
        private Color _transpColor;
        private void Awake() 
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _fullColor = _spriteRenderer.color;
            _transpColor = _fullColor;
            _transpColor.a = 0.5f;
        }

        public void StartAnimation()
        {
            StartCoroutine(CoolingDown());
        }
        private IEnumerator CoolingDown()
        {
            float endCooldownTime = Time.time + _cooldownTime;
            do
            {
                _spriteRenderer.color = _transpColor;
                yield return new WaitForSeconds(_flickTime);
                _spriteRenderer.color = _fullColor;
                yield return new WaitForSeconds(_flickTime);
            } while (Time.time + _flickTime < endCooldownTime);
        }
    }
}
