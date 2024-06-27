using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class SpecialStateAnimation : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Color _oldColor = Color.white;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _oldColor = _spriteRenderer.color;
        }

        public void StartAnimation(float animationTime, Color newColor, float flickTime = 0)
        {
            if (flickTime > 0)
            {
                StartCoroutine(FlickAnimation(animationTime, newColor, flickTime));
            }
            else
            {
                StartCoroutine(StaticAnimation(animationTime, newColor));
            }
        }
        private IEnumerator FlickAnimation(float animationTime, Color newColor, float flickTime)
        {
            float endAnimationTime = Time.time + animationTime + 0.05f;
            do
            {
                _spriteRenderer.color = newColor;
                yield return new WaitForSeconds(flickTime);
                _spriteRenderer.color = _oldColor;
                yield return new WaitForSeconds(flickTime);
            } while (Time.time + flickTime < endAnimationTime);
        }
        private IEnumerator StaticAnimation(float animationTime, Color newColor)
        {
            _spriteRenderer.color = newColor;
            yield return new WaitForSeconds(animationTime);
            _spriteRenderer.color = _oldColor;
        }
    }
}
