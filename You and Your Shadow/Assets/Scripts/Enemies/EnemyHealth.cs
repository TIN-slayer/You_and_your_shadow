using Orbs;
using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyObj;
        public static Action<string> EnemyDied;
        private bool _isGuarded = false;

        public bool GuardState
        {
            set
            {
                _isGuarded = value;
            }
        }

        public void TakeDamage()
        {
            // �������� ����� ��� ������ � ��
            if (!_isGuarded)
            {
                EnemyDied?.Invoke(tag);
                Destroy(_enemyObj);
            }
        }
    }
}
