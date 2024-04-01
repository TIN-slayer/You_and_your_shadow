using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyObj;
        public static Action<string> EnemyDied;
        public void TakeDamage()
        {
            // Добавить ивент для саунда и тд
            EnemyDied?.Invoke(tag);
            Destroy(_enemyObj);
        }
    }
}
