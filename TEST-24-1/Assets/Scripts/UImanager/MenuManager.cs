using Core;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UImanager
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            if (MainMenuMode.MenuType == 1)
            {
                _playButton.onClick?.Invoke();
            }
        }
    }
}
