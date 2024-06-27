using General;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UImanager
{
    public class MenuMode : MonoBehaviour
    {
        public static int MenuType = 0;
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            if (MenuType == 1)
            {
                _playButton.onClick?.Invoke();
            }
        }
    }
}
