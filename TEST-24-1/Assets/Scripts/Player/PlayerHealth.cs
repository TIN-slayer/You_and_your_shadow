using GameManager;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        public static event Action Lose;

        public void TakeDamage()
        {
            // �������� ����� ��� ������ � ��
            Lose.Invoke();
            Destroy(_player);
        }
    }
}
