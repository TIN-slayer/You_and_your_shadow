using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManager;
using Player;
using System;

namespace UImanager
{
    public class GeneralMenus : MonoBehaviour
    {
        private bool _isPaused = false;
        private bool _isLocked = false;
        [SerializeField] private GameObject _shadeScreen;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _winMenu;
        [SerializeField] private GameObject _loseMenu;
        public static event Action GamePause;
        public static event Action GameResume;
        private float _delayLoad = 0.1f;

        private void OnEnable()
        {
            ScenarioManager.Win += LoadWinScreen;
            PlayerHealth.Lose += LoadLoseScreen;
        }

        private void OnDisable()
        {
            ScenarioManager.Win -= LoadWinScreen;
            PlayerHealth.Lose -= LoadLoseScreen;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && !_isLocked)
            {
                if (_isPaused)
                {
                    Resume(_pauseMenu);
                }
                else
                {
                    Pause(_pauseMenu);
                }
            }
        }

        public void Resume(GameObject menu = null)
        {
            _shadeScreen.SetActive(false);
            menu?.SetActive(false);
            Time.timeScale = 1;
            _isPaused = false;
            GameResume?.Invoke();
        }

        public void Pause(GameObject menu = null)
        {
            _shadeScreen.SetActive(true);
            menu?.SetActive(true);
            Time.timeScale = 0;
            _isPaused = true;
            GamePause?.Invoke();
        }

        public void LoadLevelsMenu()
        {
            MainMenuMode.MenuType = 1;
            StartCoroutine(DelayLoadScene((int)ScenesEnum.MainMenu));
        }

        public void LoadNextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex < (int)ScenesEnum.lvl_last)
            {
                StartCoroutine(DelayLoadScene(SceneManager.GetActiveScene().buildIndex + 1));
            }
            else
            {
                LoadLevelsMenu();
            }
        }

        public void ReplayLevel()
        {
            StartCoroutine(DelayLoadScene(SceneManager.GetActiveScene().buildIndex));
        }

        private void LoadWinScreen()
        {
            _isLocked = true;
            Pause(_winMenu);
        }

        private void LoadLoseScreen()
        {
            _isLocked = true;
            Pause(_loseMenu);
        }

        // Костыль
        private IEnumerator DelayLoadScene(int scene)
        {
            yield return new WaitForSecondsRealtime(_delayLoad);
            Time.timeScale = 1;
            SceneManager.LoadScene(scene);
        }
    }
}
