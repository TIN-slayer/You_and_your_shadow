using GameManager;
using General;
using System;
using UnityEngine;

namespace Player
{
    public struct LivesChangedArgs
    {
        public int lives;
        public float cooldown;
    }


    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        public static event Action Lose;
        public static event Action<LivesChangedArgs> LivesChanged;
        [SerializeField] private int _lives;
        [SerializeField] private float _cooldown;
        [SerializeField] private Color _invulnarableColor;
        private SpecialStateAnimation _invulnarableAnimation;


        private void Awake()
        {
            _invulnarableAnimation = GetComponent<SpecialStateAnimation>();
        }

        private void Start()
        {
            LivesChanged.Invoke(new LivesChangedArgs { lives = _lives, cooldown = 0 });
        }

        public void TakeDamage()
        {
            --_lives;
            LivesChanged.Invoke(new LivesChangedArgs { lives = _lives, cooldown = _cooldown });
            _invulnarableAnimation.StartAnimation(_cooldown, _invulnarableColor);
            if (_lives == 0)
            {
                // Добавить ивент для саунда и тд
                Lose.Invoke();
                Destroy(_player);
            }
        }
    }
}
