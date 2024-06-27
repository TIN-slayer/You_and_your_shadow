using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbs
{
    public class OrbCollection : MonoBehaviour
    {
        [SerializeField] private GameObject _orb;
        public static Action<string> OrbCollected;
        public void GetCollected()
        {
            // ������ ������ ��� ����������� ��� ������ �����
            OrbCollected?.Invoke(tag);
            Destroy(_orb);
        }
    }
}
